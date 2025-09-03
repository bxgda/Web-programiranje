import { dodajRibu } from "../utils.js";

export class Opcije {
  constructor() {}

  draw(container) {
    const naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovDiv");

    const lblNaslov = document.createElement("label");
    lblNaslov.innerHTML = "Riba";

    naslovDiv.append(lblNaslov);
    container.append(naslovDiv);

    const polja = [
      {
        label: "Vrsta:",
        input: "text",
        class: "vrsta-input",
      },
      {
        label: "Masa:",
        input: "number",
        class: "masa-input",
      },
      {
        label: "Starost",
        input: "number",
        class: "starost-input",
      },
    ];

    polja.forEach((polje) => {
      const box = document.createElement("div");
      box.classList.add("boxOpcije");

      const lbl = document.createElement("label");
      lbl.innerHTML = polje.label;

      const inp = document.createElement("input");
      inp.classList.add(polje.class);
      inp.type = polje.input;

      box.append(lbl, inp);
      container.append(box);
    });

    const dugme = document.createElement("button");
    dugme.innerHTML = "Dodaj ribu";
    dugme.onclick = this.dodajRibu;

    container.append(dugme);
  }

  dodajRibu = async () => {
    const vrsta = document.querySelector(".vrsta-input");
    const masa = document.querySelector(".masa-input");
    const starost = document.querySelector(".starost-input");

    const { error, data } = await dodajRibu(
      vrsta.value,
      masa.value,
      starost.value
    );
    if (error) {
      alert(error);
    } else {
      // osvezi sve select-e (dropdown-e)... mora u foreach iz meni nepoznatih razloga
      const selectElement = document.querySelectorAll("select");
      selectElement.forEach((select) => {
        const opt = document.createElement("option");
        opt.value = data.id;
        opt.textContent = data.vrsta;
        select.append(opt);
      });
    }

    vrsta.value = "";
    masa.value = "";
    starost.value = "";
  };
}
