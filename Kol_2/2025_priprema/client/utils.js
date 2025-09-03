import { Rezervoar } from "./models/rezervoar.js";
import { RibaSelect } from "./models/ribaSelect.js";
import { RibaURezervoaru } from "./models/ribaURezervoaru.js";

const baseUrl = "https://localhost:7248/Akvarijum";

export async function vratiRezervoare() {
  try {
    const response = await fetch(`${baseUrl}/PreuzmiRezervoare`);
    const data = await response.json();

    const rezervoari = data.map((rezervoar) => {
      const ribe = rezervoar.ribe.map((riba) => {
        return new RibaURezervoaru(
          riba.idSpoja,
          riba.nazivRibe,
          riba.brojKomada,
          riba.masa
        );
      });
      return new Rezervoar(
        rezervoar.id,
        rezervoar.kapacitet,
        rezervoar.sifra,
        ribe
      );
    });

    return rezervoari;
  } catch (error) {
    console.error("preuzimanje rezervoara: ", error);
    return [];
  }
}

export async function vratiSveRibe() {
  try {
    const response = await fetch(`${baseUrl}/PreuzmiSveRibe`);
    const data = await response.json();

    const ribe = data.map((riba) => {
      return new RibaSelect(riba.id, riba.vrsta);
    });

    return ribe;
  } catch (error) {
    console.error("vrati sve ribe: ", error);
  }
}

export async function dodajRibu(vrsta, masa, starost) {
  const riba = {
    vrsta,
    masa,
    starost,
  };
  try {
    const response = await fetch(`${baseUrl}/DodavanjeRibe`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(riba),
    });

    const data = await response.json();

    return {
      error: null,
      data: data,
    };
  } catch (error) {
    console.error("dodavanje ribe u bazu: ", error);
  }
}

export async function izmeni(idSpoja, broj, masa, datum) {
  try {
    const response = await fetch(
      `${baseUrl}/IzmenaJedinke/${idSpoja}/${broj}/${masa}/${datum}`,
      {
        method: "PUT",
      }
    );

    if (!response.ok) {
      return false;
    }

    const data = await response.json();
    alert("uspesna izmena jedinke: ", data);

    return true;
  } catch (error) {
    console.error("izmena ribe: ", error);
  }
}

export async function dodajRibuURezervoar(
  rezervoarID,
  ribaID,
  brojJedinki,
  datum,
  masa
) {
  try {
    const response = await fetch(
      `${baseUrl}/DodavanjeRibeURezervoar/${rezervoarID}/${ribaID}/${brojJedinki}/${datum}/${masa}`,
      {
        method: "POST",
      }
    );

    if (!response.ok) {
      const errorMsg = await response.text();
      return {
        error: errorMsg,
        data: null,
      };
    }

    const data = await response.json();
    return {
      error: null,
      data: data,
    };
  } catch (error) {
    console.log("dodajRibuURezervoar error:", error);
    return {
      error: "Neuspesno dodavanje ribe u rezervoar",
      data: null,
    };
  }
}
