import { Chat } from "./models/chat.js";
import { Korisnik } from "./models/korisnik.js";
import { Soba } from "./models/soba.js";

export async function vratiKorisnike() {
  try {
    const response = await fetch(`https://localhost:7080/Chat/VratiKorisnike`);
    const data = await response.json();

    const korisnici = data.map((korisnik) => {
      return new Korisnik(korisnik.id, korisnik.korisnickoIme);
    });
    return korisnici;
  } catch (error) {
    console.error(error);
  }
}

export async function vratiSobe() {
  try {
    const response = await fetch(`https://localhost:7080/Chat/VratiSobe`);
    const data = await response.json();

    const sobe = data.map((soba) => {
      return new Soba(soba.id, soba.naziv);
    });
    return sobe;
  } catch (error) {
    console.error(error);
  }
}

export async function vratiClanoveSobe(idSobe) {
  try {
    const response = await fetch(
      `https://localhost:7080/Chat/VratiClanoveSobe/${idSobe}`
    );
    const data = await response.json();

    const clanoviSobe = data.map((clan) => {
      return new Chat(clan.id, clan.korisnickoIme, clan.nadimak, clan.boja);
    });
    return clanoviSobe;
  } catch (error) {
    console.error(error);
  }
}

export async function ubaciKorisnikaUSobu(
  sobaNaziv,
  idkorisnika,
  nadimak,
  boja
) {
  try {
    // ovo ovako treba sa encode da bi # iz boje prebacio u neki tamo znak i eventualni razmak u nadimak ili soba
    const url = `https://localhost:7080/Chat/UbaciKorisnikaUSobu/${encodeURIComponent(
      sobaNaziv
    )}/${idkorisnika}/${encodeURIComponent(nadimak)}/${encodeURIComponent(
      boja
    )}`;
    const response = await fetch(url, {
      method: "POST",
    });
    if (response.error) return -1;
    return 1;
  } catch (error) {
    console.error(error);
  }
}

export async function prebrojJedinstvene(sobaId) {
  try {
    const odgovor = await fetch(
      `https://localhost:7080/Chat/PrebrojJedinstvene/${sobaId}`
    );
    const broj = await odgovor.json();
    return broj;
  } catch (error) {
    console.error(error);
  }
}
