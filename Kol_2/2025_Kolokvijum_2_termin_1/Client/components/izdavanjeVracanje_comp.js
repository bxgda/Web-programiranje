import {
  izdajVratiKnjigu,
  najcitanijaKnjiga,
  vratiPretrazeneKnjige,
} from "../utils.js";

export class IzdavanjeVracanjeComp {
  constructor(bibliotekaID) {
    this.bibliotekaID = bibliotekaID;
    this.izabraneKnjige = [];
  }

  draw(container) {
    // naslov
    const naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovDiv");
    const naslovLabel = document.createElement("h3");
    naslovLabel.innerHTML = "Izdavanje/Vraćanje ";

    naslovDiv.append(naslovLabel);

    container.append(naslovDiv);

    // div za input i dugme za pretrazivanje
    const inputDiv = document.createElement("div");
    inputDiv.classList.add("inputDiv");

    // input za pretragu
    const inputNaziv = document.createElement("input");
    inputNaziv.classList.add("inputNaziv");
    inputNaziv.type = "text";

    // dugme za pretragu
    const dugmeNadji = document.createElement("button");
    dugmeNadji.innerHTML = "Nadji";
    dugmeNadji.classList.add("dugmeNadji");
    dugmeNadji.onclick = () => this.nadjiKnjigu();

    inputDiv.append(inputNaziv, dugmeNadji);

    // rezultati koji se vrate
    const selectRezultat = document.createElement("select");
    selectRezultat.classList.add("selectRezultat");

    // ovde se crta dugme izdaj/vrati
    const akcijaDiv = document.createElement("div");
    akcijaDiv.classList.add("akcijaDiv");

    // dugme za izdavanje i vracanje knjige, ako nije nista pretrazeno ne radi nista
    const dugme = document.createElement("button");
    dugme.innerHTML = "izdaj/vrati";
    dugme.classList.add("dugmeAkcija");
    akcijaDiv.append(dugme);

    // kad se napuni select na change se radi navedena funkcija
    selectRezultat.addEventListener("change", () => {
      this.prikaziDugme(dugme, selectRezultat.value);
    });

    container.append(naslovDiv, inputDiv, selectRezultat, akcijaDiv);
  }

  prikaziDugme = (dugme, knjigaId) => {
    // nadjemo knjigu koja je izabrana
    const knjiga = this.izabraneKnjige.find((k) => k.id === parseInt(knjigaId));

    // u zavisnosti da li je izdata ili ne prikazujemo odgovarajuci tekst
    if (knjiga && knjiga.izdata) {
      dugme.innerHTML = "Vrati";
    } else {
      dugme.innerHTML = "Izdaj";
    }

    // dodajemo funkcionalnost
    dugme.onclick = () => this.izdajVratiKnjigu(knjigaId);
  };

  nadjiKnjigu = async () => {
    const odgovarajuciDiv = document.querySelector(
      `.sredniDiv-${this.bibliotekaID}`
    );

    const input = odgovarajuciDiv.querySelector(".inputNaziv");
    const select = odgovarajuciDiv.querySelector(".selectRezultat");

    // napunimo listu sa knjigama koje su pretrazene
    this.izabraneKnjige = await vratiPretrazeneKnjige(
      this.bibliotekaID,
      input.value
    );

    // izbrisemo sve staro iz selecta
    select.innerHTML = "";

    // za svaku knjigu ubacimo u select
    this.izabraneKnjige.forEach((knjiga) => {
      let option = document.createElement("option");
      option.value = knjiga.id;
      option.innerHTML = `${knjiga.naslov} - ${knjiga.autor}`;
      select.appendChild(option);
    });

    const akcijaDiv = odgovarajuciDiv.querySelector(".akcijaDiv");
    const dugme = akcijaDiv.querySelector(".dugmeAkcija");

    // postavi dugme na osnovu prve knjige u selectu i dodamo joj funkcionalnost
    if (this.izabraneKnjige.length > 0) {
      const prvaKnjiga = this.izabraneKnjige[0];
      if (prvaKnjiga.izdata) {
        dugme.innerHTML = "Vrati";
      } else {
        dugme.innerHTML = "Izdaj";
      }
      dugme.onclick = () => this.izdajVratiKnjigu(prvaKnjiga.id);
    }
  };

  izdajVratiKnjigu = async (knjigaId) => {
    // pozovemo funkciju da se izvrsi
    await izdajVratiKnjigu(this.bibliotekaID, knjigaId);

    const odgovarajuciDiv = document.querySelector(
      `.sredniDiv-${this.bibliotekaID}`
    );
    const akcijaDiv = odgovarajuciDiv.querySelector(".akcijaDiv");
    const dugme = akcijaDiv.querySelector(".dugmeAkcija");

    // nakon izvrsenja funkcije samo zamenimo tekst na dugmetu
    if (dugme.innerHTML === "Izdaj") {
      dugme.innerHTML = "Vrati";
    } else {
      dugme.innerHTML = "Izdaj";
    }

    // ponovo pozovemo funkciju za najcitaniju knjigu u desnom divu jer je mozda doslo do promene
    const desniDiv = document.querySelector(`.desniDiv-${this.bibliotekaID}`);
    const najcitanijaLabel = desniDiv.querySelector("h4");
    najcitanijaLabel.innerHTML = await najcitanijaKnjiga(this.bibliotekaID);
  };
}
