(function () {
    const ingredients = JSON.parse(document.getElementById("ingredientsAsJson").value);
    const searchInput = document.getElementById("search-input");
    const selectElement = document.getElementById("Ingredients");
    const submitButton = document.getElementById("submit");
    let filteredIngredients = ingredients;
    let isFiltered = false;

    document.querySelector("form").addEventListener("reset", e => {
        e.preventDefault();
        searchInput.value = "";
        ingredients.forEach(i => i.selected = false);
        isFiltered = false;
        setSubmitButtonDisabledState(isFiltered);
        populateSelect(selectElement, ingredients);
    });

    document.getElementById("filter").addEventListener("click", e => {
        e.preventDefault();
        const search = searchInput.value.trim();

        if (search.length === 0) {
            return;
        }

        isFiltered = true;
        setSubmitButtonDisabledState(isFiltered);
        filteredIngredients = ingredients.filter(i => i.name.toLowerCase().includes(search.toLowerCase()));
        populateSelect(selectElement, filteredIngredients);
    });

    document.getElementById("reset-filter").addEventListener("click", e => {
        e.preventDefault();
        searchInput.value = "";
        isFiltered = false;
        setSubmitButtonDisabledState(isFiltered);
        populateSelect(selectElement, ingredients);
    });

    selectElement.addEventListener("change", e => {
        e.preventDefault();
        if (isFiltered) {
            const children = Array.from(selectElement.children);
            syncSelection(filteredIngredients, children);
        } else {
            const children = Array.from(selectElement.children);
            syncSelection(ingredients, children);
        }
    });

    function populateSelect(select, items) {
        select.length = 0;

        for (const item of items.sort((a, b) => a.name.localeCompare(b.name))) {
            var option = document.createElement("option");
            option.setAttribute("value", item.id);
            option.innerText = item.name;

            if (item.selected) {
                option.setAttribute("selected", true);
            } else {
                option.removeAttribute("selected");
            }

            select.appendChild(option);
        }
    }

    function syncSelection(collection, options) {
        options.forEach(option => {
            const ingredient = collection.find(i => i.id === Number(option.value));
            if (ingredient !== undefined) {
                ingredient.selected = option.selected;
            }
        });
    }

    function setSubmitButtonDisabledState(isDisabled) {
        if (isDisabled) {
            submitButton.setAttribute("disabled", true);
        } else {
            submitButton.removeAttribute("disabled");
        }
    }

    populateSelect(selectElement, ingredients);
})();
