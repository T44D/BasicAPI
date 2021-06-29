const animals = [
    { name: "Chi", species: "Cat", class: { name: "Mammal" } },
    { name: "Pochita", species: "Demon", class: { name: "Unknown" } },
    { name: "Hachiko", species: "Dog", class: { name: "Mammal" } },
    { name: "Kuma", species: "Bear", class: { name: "Mammal" } },
    { name: "Gon", species: "Dinosaur", class: { name: "Reptile" } },
    { name: "Nemo", species: "Ocellaris Clownfish", class: { name: "Fish" } },
    { name: "Dory", species: "Royal Blue Tang", class: { name: "Fish" } },
    { name: "Keroppi", species: "Frog", class: { name: "Amphibian" } },
    { name: "Mighty", species: "Eagle", class: { name: "Bird" } },
];
console.log(filterSpecies("Cat"));
manipulationString()
console.log(animals);


function filterSpecies(species) {
    let temp = [];
    for (let i = 0; i < animals.length; i++) {
        if (animals[i].species == species) {
            temp.push(animals[i]);
        }
    }
    return temp;
}

function manipulationString() {
    for (let i = 0; i < animals.length; i++) {
        if (animals[i].class.name != "Mammal") {
            animals[i].class.name = "Non-Mammal";
        }
    }
}