import { Prodavnica } from "./models/prodavnica.js";
import { Proizvod } from "./models/proizvod.js";

export async function vratiProdavnice() {
  try {
    const response = await fetch(`http://localhost:5086/VratiProdavnice`);
    const data = await response.json();

    const prodavnice = data.map((prod) => {
      const proizvodi = prod.listaProizvoda.map((proiz) => {
        return new Proizvod(
          proiz.proizvodID,
          proiz.naziv,
          proiz.kategorija,
          proiz.cena,
          proiz.dostupnaKolicina
        );
      });
      return new Prodavnica(prod.naziv, prod.lokacija, prod.telefon, proizvodi);
    });
    return prodavnice;
  } catch (error) {
    console.error(error);
  }
}

export async function prodajProizvod(
  nazivProdavnice,
  nazivProizvoda,
  kolicina
) {
  try {
    const response = await fetch(
      `http://localhost:5086/ProdajProizvod/${nazivProdavnice}/${nazivProizvoda}/${kolicina}`,
      {
        method: "POST",
      }
    );
    const data = await response.json();

    if (data.error) {
      alert(data.error);
      return -1;
    }

    return data.kupljenaKolicina;
  } catch (error) {
    console.error(error);
  }
}

export async function dodajNoviProizvod(
  naziv,
  kategorija,
  cena,
  kolicina,
  nazivProdavnice
) {
  try {
    const response = await fetch(
      `http://localhost:5086/DodajProizvod/${naziv}/${kategorija}/${cena}/${kolicina}/${nazivProdavnice}`,
      {
        method: "PUT",
      }
    );
  } catch (error) {
    console.error(error);
  }
}
