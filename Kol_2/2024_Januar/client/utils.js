import { Automobil } from "./models/automobil.js";

export async function vratiAutomobile() {
  try {
    const response = await fetch(
      `http://localhost:5096/RentCar/PrikaziAutomobile`
    );
    const data = await response.json();

    const automobili = data.map((auto) => {
      return new Automobil(
        auto.automobilID,
        auto.model,
        auto.predjenaKilometraza,
        auto.godiste,
        auto.cenaPoDanu,
        auto.iznajmljen
      );
    });
    return automobili;
  } catch (error) {
    console.error(error);
  }
}

export async function iznajmiAuto(
  ime,
  jmbg,
  brojDozvole,
  brojDana,
  model,
  kilometraza,
  godiste,
  cenaPoDanu
) {
  try {
    const response = await fetch(
      `http://localhost:5096/RentCar/IznajmiAutomobil`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          ime: ime,
          prezime: ime,
          jmbg: jmbg,
          brVozacke: brojDozvole,
          model: model,
          kilometraza: kilometraza,
          godiste: godiste,
          cena: cenaPoDanu,
          brDana: brojDana,
        }),
      }
    );
    if (!response.ok) return false;
    const data = await response.text();
    console.log(data);
    return true;
  } catch (error) {
    console.error(error);
  }
}

export async function filtrirajAutomobile(predjenaKilometraza, godiste, model) {
  try {
    const response = await fetch(
      `http://localhost:5096/RentCar/Filtriraj?Model=${model}&Kilometraza=${predjenaKilometraza}&Godiste=${godiste}`
    );
    const data = await response.json();

    const automobili = data.map((auto) => {
      return new Automobil(
        auto.automobilID,
        auto.model,
        auto.predjenaKilometraza,
        auto.godiste,
        auto.cenaPoDanu,
        auto.iznajmljen
      );
    });
    return automobili;
  } catch (error) {
    console.error(error);
  }
}
