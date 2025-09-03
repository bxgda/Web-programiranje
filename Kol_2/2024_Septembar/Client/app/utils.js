import { Stan } from "./models/stan.js";
import { Racun } from "./models/racun.js";

export async function fetchStanoviIds() {
  try {
    const response = await fetch(
      "https://localhost:7080/Stan/VratiIDeveSvihStanova"
    );
    const data = await response.json();
    return data;
  } catch (error) {
    console.error(error);
  }
}

export async function fetchStanById(id) {
  try {
    const response = await fetch(`https://localhost:7080/Stan/VratiStan/${id}`);
    const data = await response.json();

    let racuni = [];
    data.racuni.forEach((racun) => {
      racuni.push(
        new Racun(
          racun.mesec,
          racun.cenaVode,
          racun.cenaZajednickeStruje,
          racun.cenaKomunalija,
          racun.placen
        )
      );
    });

    const stan = new Stan(
      data.stan.id,
      data.stan.vlasnik,
      data.stan.povrsina,
      data.stan.brojClanova,
      racuni
    );

    return stan;
  } catch (error) {
    console.error(error);
  }
}

export async function fetchUkupnoZaduzenje(idStana) {
  try {
    const response = await fetch(
      `https://localhost:7080/Stan/IzracunajUkupnoZaduzenje/${idStana}`
    );
    const data = await response.json();
    return data;
  } catch (error) {
    console.error(error);
  }
}
