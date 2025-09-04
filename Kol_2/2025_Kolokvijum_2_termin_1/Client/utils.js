import { Biblioteka } from "./models/biblioteka.js";
import { Knjiga } from "./models/knjiga.js";

const baseUrl = "http://localhost:5215/Biblioteka";

export async function vratiBiblioteke() {
  try {
    const response = await fetch(`${baseUrl}/PreuzmiBiblioteke`);
    const data = await response.json();

    const biblioteke = data.map(
      (biblioteka) =>
        new Biblioteka(
          biblioteka.id,
          biblioteka.imeBiblioteke,
          biblioteka.adresa,
          biblioteka.email
        )
    );
    return biblioteke;
  } catch (error) {
    console.error("vrati biblioteke: ", error);
  }
}

export async function vratiPretrazeneKnjige(bibliotekaId, tekst) {
  try {
    const response = await fetch(
      `${baseUrl}/PronadjiKnjigePoKriterijumu/${bibliotekaId}/${tekst}`
    );
    const data = await response.json();

    const knjige = data.map(
      (knjiga) =>
        new Knjiga(knjiga.id, knjiga.naslov, knjiga.autor, knjiga.izdata)
    );

    return knjige;
  } catch (error) {
    console.error("vrati pretrazene knjige: ", error);
  }
}

export async function dodajKnjigu(
  bibliotekaId,
  Naslov,
  Autor,
  GodinaIzdavanja,
  Izdavac,
  BrojUEvidenciji
) {
  const novaKnjiga = {
    Naslov,
    Autor,
    GodinaIzdavanja,
    Izdavac,
    BrojUEvidenciji,
  };
  try {
    const response = await fetch(`${baseUrl}/DodavanjeKnjige/${bibliotekaId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(novaKnjiga),
    });
  } catch (error) {
    console.error("dodaj knjigu: ", error);
  }
}

export async function izdajVratiKnjigu(bibliotekaId, knjigaId) {
  try {
    const response = await fetch(
      `${baseUrl}/IzdavanjeVracanjeKnjige/${bibliotekaId}/${knjigaId}`,
      {
        method: "PUT",
      }
    );
  } catch (error) {
    console.error("izdaj vrati knjigu: ", error);
  }
}

export async function najcitanijaKnjiga(bibliotekaId) {
  try {
    const response = await fetch(`${baseUrl}/Najcitanija/${bibliotekaId}`, {});

    const data = await response.text();
    return data;
  } catch (error) {
    console.error("Greška prilikom poziva API-ja:", error);
    return null;
  }
}
