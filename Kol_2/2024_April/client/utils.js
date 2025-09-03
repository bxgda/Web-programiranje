import { Projekcija } from "./models/projekcija.js";
import { Sediste } from "./models/sediste.js";

const baseUrl = "https://localhost:7080/Bioskop";

export async function vratiProjekcije() {
  try {
    const response = await fetch(`${baseUrl}/buduceProjekcije`);
    const data = await response.json();

    const projekcije = data.map((projekcija) => {
      return new Projekcija(projekcija.id);
    });

    return projekcije;
  } catch (error) {
    console.error("vrati projekcije: ", error);
  }
}

export async function vratiBrojRedova(idProjekcije) {
  try {
    const response = await fetch(`${baseUrl}/vratiBrojRedova/${idProjekcije}`);
    const data = await response.json();

    return data;
  } catch (error) {
    console.error("vrati broj redova: ", error);
  }
}

export async function vratiSedistaReda(idProjekcije) {
  try {
    const reponse = await fetch(
      `${baseUrl}/vratiBrojSedistURedu/${idProjekcije}`
    );
    const data = reponse.json();

    return data;
  } catch (error) {
    console.error("vrati sedista u redu: ", error);
  }
}

export async function zauzetaSedista(idProjekcije) {
  try {
    const response = await fetch(`${baseUrl}/zauzetaSedista/${idProjekcije}`);
    const data = await response.json();

    const sedista = data.map((_sediste) => {
      return new Sediste(_sediste.red, _sediste.sediste, true);
    });

    return sedista;
  } catch (error) {}
}

// https://localhost:7080/Bioskop/kupiKartu/4/4/150/1234

export async function vratiNaslovProjekcije(id) {
  try {
    const response = await fetch(`${baseUrl}/vratiProjekciju/${id}`);
    const data = await response.text();

    return data;
  } catch (error) {
    console.error("vrati naslov projekcije: ", error);
  }
}

export async function kupiKartu(red, sediste, cena, sifraProjekcije) {
  try {
    const response = await fetch(
      `${baseUrl}/kupiKartu/${red}/${sediste}/${cena}/${sifraProjekcije}`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    if (response.ok) return 1;
    else return -1;
  } catch (error) {
    console.error("kupi kartu: ", error);
  }
}
