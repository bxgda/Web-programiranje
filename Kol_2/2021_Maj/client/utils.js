import { Fabrika } from "./models/fabrika.js";
import { Silos } from "./models/silos.js";

export async function vratiFabrike() {
  try {
    const reponse = await fetch(`http://localhost:5121/VratiFabrike`);
    const data = await reponse.json();

    const fabrike = data.map((fab) => {
      const silosi = fab.listaSilosa.map((sil) => {
        return new Silos(
          sil.silosID,
          sil.oznaka,
          sil.maxKapacitet,
          sil.trenutnaKolicina
        );
      });
      return new Fabrika(fab.fabrikaID, fab.naziv, silosi);
    });
    return fabrike;
  } catch (error) {
    console.error(error);
  }
}

export async function vratiSilose(nazivFabrike) {
  try {
    const reponse = await fetch(
      `http://localhost:5121/VratiSilose/${nazivFabrike}`
    );
    const data = await reponse.json();

    const silosi = data.silosi.map((sil) => {
      return new Silos(
        sil.silosID,
        sil.oznaka,
        sil.maxKapacitet,
        sil.trenutnaKolicina
      );
    });
    return silosi;
  } catch (error) {
    console.error(error);
  }
}

export async function promeniPopunjenostSilosa(
  nazivFabrike,
  oznakaSiloas,
  novaKolicina
) {
  try {
    const reponse = await fetch(
      `http://localhost:5121/IzmeniKolicinu/${nazivFabrike}/${oznakaSiloas}/${novaKolicina}`,
      {
        method: "PUT",
      }
    );
    const data = await reponse.json();

    if (data.error) {
      alert(data.error);
      return -1;
    }

    return data.novaKolicina;
  } catch (error) {
    console.error(error);
    return -1;
  }
}
