let brands = [];
let connection = null;
getdata();
setupSignalR();


function setupSignalR() {
     connection = new signalR.HubConnectionBuilder()
         .withUrl("http://localhost:20347/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandCreated", (user, message) => {
        getdata();
    });

    connection.on("BrandDeleted", (user, message) => {
        getdata();
    });

    connection.on("BrandUpdated", (user, message) => {
        getdata();
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

async function getdata() {
    await fetch('http://localhost:20347/stock/ListBrands')
        .then(x => x.json())
        .then(y => {
            brands = y;
            console.log(brands);
            display();
        });
}

function display() {
    document.getElementById('Brandresultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('Brandresultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.country + "</td><td>" + t.ceo + "</td><td>" + t.source + "</td><td>" + t.foundation + "</td><td>" +
        `<button type="button" onclick="remove(${t.id})">Delete</button>` + `<button type="button" onclick="Update(${t.id})">Update</button>`
            +"</td></tr>";
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
            getdata();
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
            getdata();
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
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Id: id, Name: name, Country: country, Ceo: ceo, Source: source })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}