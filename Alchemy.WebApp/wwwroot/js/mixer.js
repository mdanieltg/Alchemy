(function () {
    const jsonList = document.getElementById("ingredients").value;
    const ingredients = JSON.parse(jsonList);
    const selectedIngredients = new Set();
    const select = document.querySelector("form select");
    const input = document.getElementById("ingredients-input");
    const dataList = document.getElementById("readable-ingredients");

    document.getElementById("add-ingredient").addEventListener("click", (e) => {
        e.preventDefault();

        const value = input.value.trim().toLowerCase();
        const ingredient = ingredients.find(i => i.name.toLowerCase() === value);

        if (ingredient) {
            selectedIngredients.add(ingredient);
            updateSelect(select, selectedIngredients);
        }

        input.value = "";
    });

    document.querySelector("form").addEventListener("submit", (e) => {
        for (const option of select.options) {
            option.setAttribute("selected", true);
        }
    });

    function updateSelect(selectlist, ingredients) {
        selectlist.options.length = 0;

        for (const ingredient of ingredients) {
            const option = document.createElement("option");
            option.setAttribute("value", ingredient.id);
            option.innerText = ingredient.name;
            selectlist.appendChild(option);
        }
    }

    function populateDataList(datalist, ingredients) {
        for (const item of ingredients) {
            const option = document.createElement("option");
            option.setAttribute("value", item.name);
            datalist.appendChild(option);
        }
    }

    populateDataList(dataList, ingredients);

})();
