let brands = [];
let brand = null;
let models = [];
let model = null;
let connection = null;
getBrandData();
getModelData();
setupSignalR();


function setupSignalR() {
     connection = new signalR.HubConnectionBuilder()
         .withUrl("http://localhost:20347/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandCreated", (user, message) => {
        getBrandData();
    });

    connection.on("BrandDeleted", (user, message) => {
        getBrandData();
    });

    connection.on("BrandUpdated", (user, message) => {
        getBrandData();
    });

    connection.on("ModelCreated", (user, message) => {
        getModelData();
    });

    connection.on("ModelDeleted", (user, message) => {
        getModelData();
    });

    connection.on("ModelUpdated", (user, message) => {
        getModelData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        console.log("SignalR NOT CONNECTED.");
        setTimeout(start, 5000);
    }
};

async function getBrandData() {
    await fetch('http://localhost:20347/stock/ListBrands')
        .then(x => x.json())
        .then(y => {
            brands = y;
            console.log(brands);
            displayBrand();
        });
}
async function getModelData() {
    await fetch('http://localhost:20347/stock/ListModels')
        .then(x => x.json())
        .then(y => {
            models = y;
            console.log(models);
            displayModel();
        });
}
//GET: stock / ListBrandByID / "id"
async function getOneBrand() {
    let id = document.getElementById('brandid').value;
    await fetch('http://localhost:20347/stock/ListBrandByID/' + id)
        .then(x => x.json())
        .then(y => {
            brand = y;
            console.log(brand);
            brands = [];
            brands.push(brand);
            displayBrand();
        });
}
async function getOneModel() {
    let id = document.getElementById('modelid').value;
    await fetch('http://localhost:20347/stock/ListModelByID/' + id)
        .then(x => x.json())
        .then(y => {
            model = y;
            console.log(model);
            models = [];
            models.push(model);
            displayModel();
        });
}


function displayBrand() {
    document.getElementById('Brandresultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('Brandresultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.country + "</td><td>" + t.ceo + "</td><td>" + t.source + "</td><td>" + t.foundation + "</td><td>" +
        `<button type="button" onclick="remove(${t.id})">Delete</button>` + `<button type="button" onclick="Update(${t.id})">Update</button>`
            +"</td></tr>";
    });
}
function displayModel() {
    document.getElementById('Modelresultarea').innerHTML = "";
    models.forEach(t => {
        document.getElementById('Modelresultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.brand.name + "</td><td>" + t.name + "</td><td>" + t.modelName + "</td><td>" + t.size + "</td><td>" + t.color + "</td><td>" + t.price + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` + `<button type="button" onclick="Update(${t.id})">Update</button>`
            + "</td></tr>";
    });
}
// Post: manager/Insert{entityname}
// PUT: manager/Update{entityname}
// DELETE: manager/{entityName}/{id}
function remove(id) {
    console.log("Remove started.");
    fetch('http://localhost:20347/manager/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getBrandData();
        })
        .catch((error) => { console.error('Error:', error); });

}
// Post: manager/Insert{entityname}
function create() {
    console.log("Create started.");
    let name = document.getElementById('brandname').value;
    let country = document.getElementById('brandcountry').value;
    let ceo = document.getElementById('brandceo').value;
    let source = document.getElementById('brandsource').value;
    console.log(name, country, ceo, source);
    fetch('http://localhost:20347/manager/InsertBrand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: name,  Country: country, Ceo: ceo, Source: source })
        })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getBrandData();
        })
        .catch((error) => { console.error('Error:', error); });
}
function Update(id) {
    console.log("Update started.");
    let name = document.getElementById('brandname').value;
    let country = document.getElementById('brandcountry').value;
    let ceo = document.getElementById('brandceo').value;
    let source = document.getElementById('brandsource').value;
    console.log(name, country, ceo, source);
    fetch('http://localhost:20347/manager/UpdateBrand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Id: id, Name: name, Country: country, Ceo: ceo, Source: source })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getBrandData();
        })
        .catch((error) => { console.error('Error:', error); });
}