import { dodajNoviProizvod, vratiProdavnice } from "../utils.js";
import { ProdavnicaComponent } from "./prodavnica_component.js";

export class Opcije {
  constructor(nazivFabrike, prodavnicaContainer) {
    this.nazivFabrike = nazivFabrike;
    this.prodavnicaContainer = prodavnicaContainer;
  }

  draw(container) {
    // hard-code

    // let lblNazivProizvoda = document.createElement("label");
    // lblNazivProizvoda.innerHTML = "Naziv:";

    // let inputNaziv = document.createElement("input");
    // inputNaziv.classList.add(`inputNaziv-${this.fabrikaID}`);
    // inputNaziv.type = "text";

    // let lblKategorija = document.createElement("label");
    // lblKategorija.innerHTML = " Kategorija:";

    // let inputKategorija = document.createElement("input");
    // inputKategorija.classList.add(`inputKategorija-${this.nazivFabrike}`);
    // inputKategorija.type = "text";

    // let lblCena = document.createElement("label");
    // lblCena.innerHTML = "Cena:";

    // let inputCena = document.createElement("input");
    // inputCena.classList.add(`inputCena-${this.fabrikaID}`);
    // inputCena.type = "number";

    // let lblKolicina = document.createElement("label");
    // lblKolicina.innerHTML = "Količina:";

    // let inputKolicina = document.createElement("input");
    // inputKolicina.type = "number";
    // inputKolicina.classList.add(`inputKolicina-${this.fabrikaID}`);

    // container.append(
    //   lblNazivProizvoda,
    //   inputNaziv,
    //   lblKategorija,
    //   inputKategorija,
    //   lblCena,
    //   inputCena,
    //   lblKolicina,
    //   inputKolicina
    // );

    this.container = container;

    container.innerHTML = "";

    let naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovDiv");

    let naslovProdavnice = document.createElement("label");
    naslovProdavnice.innerHTML = "Upis proizvoda";
    naslovDiv.appendChild(naslovProdavnice);

    let contentOptDiv = document.createElement("div");
    contentOptDiv.classList.add("contentOptDiv");

    const fields = [
      {
        label: "Naziv:",
        type: "text",
        class: `inputNaziv-${this.nazivFabrike}`,
      },
      {
        label: "Kategorija:",
        type: "text",
        class: `inputKategorija-${this.nazivFabrike}`,
      },
      {
        label: "Cena:",
        type: "number",
        class: `inputCena-${this.nazivFabrike}`,
      },
      {
        label: "Količina:",
        type: "number",
        class: `inputKolicina-${this.nazivFabrike}`,
      },
    ];

    fields.forEach((field) => {
      let lbl = document.createElement("label");
      lbl.innerHTML = field.label;
      contentOptDiv.appendChild(lbl);

      let input = document.createElement("input");
      input.type = field.type;
      input.classList.add(field.class);
      input.classList.add("optInput");
      contentOptDiv.appendChild(input);
    });

    let dugme = document.createElement("button");
    dugme.classList.add("dugmeOpt");
    dugme.innerHTML = "Dodaj proizvod";
    dugme.onclick = this.dodajProizvod;
    contentOptDiv.appendChild(dugme);

    container.append(naslovDiv, contentOptDiv);
  }

  dodajProizvod = async () => {
    const naziv = document.querySelector(
      `.inputNaziv-${this.nazivFabrike}`
    ).value;
    const kategorija = document.querySelector(
      `.inputKategorija-${this.nazivFabrike}`
    ).value;
    const cena = document.querySelector(
      `.inputCena-${this.nazivFabrike}`
    ).value;
    const kolicina = document.querySelector(
      `.inputKolicina-${this.nazivFabrike}`
    ).value;

    if (!naziv || !kategorija || !cena || !kolicina) {
      alert("Popuni sva polja!");
      return;
    }

    // ovo mozda nije najoptimalnije ali nisam mogao drugacije uzevsi u obzir backend a i mozda nisam ni znao bolje

    await dodajNoviProizvod(
      naziv,
      kategorija,
      cena,
      kolicina,
      this.nazivFabrike
    );

    // ovo bas ne valja
    const prodavnice = await vratiProdavnice();
    const prodavnica = prodavnice.find((p) => p.naziv === this.nazivFabrike);
    let prodavnicaComp = new ProdavnicaComponent(prodavnica);
    prodavnicaComp.draw(this.prodavnicaContainer);

    this.draw(this.container);
  };
}
