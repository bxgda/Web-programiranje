export class Racun {
  constructor(mesec, cenavode, cenaZajednickeStruje, cenaKomunalija, placen) {
    this.mesec = mesec;
    this.cenavode = cenavode;
    this.cenaZajednickeStruje = cenaZajednickeStruje;
    this.cenaKomunalija = cenaKomunalija;
    this.placen = placen;
  }

  // ovo nije dobro sto se u modelu radi crtanje ali ok tad nismo znali bolje a i realno
  // oni na casovima i pripremama nisu nista bolji

  draw(container) {
    const box = document.createElement("div");
    box.classList.add("racunBox");
    if (this.placen == "Da") {
      box.classList.add("placen");
    } else {
      box.classList.add("neplacen");
    }

    const polja = [
      {
        label: "Mesec:",
        value: this.mesec,
      },
      {
        label: "Voda:",
        value: this.cenavode,
      },
      {
        label: "Zajednička struja:",
        value: this.cenaZajednickeStruje,
      },
      {
        label: "Komunalne usluge:",
        value: this.cenaKomunalija,
      },
      {
        label: "Plaćen:",
        value: this.placen,
      },
    ];

    polja.forEach((polje) => {
      let lbl = document.createElement("label");
      lbl.innerHTML = polje.label;

      let lblValue = document.createElement("label");
      lblValue.innerHTML = polje.value;

      box.append(lbl, lblValue);
    });

    container.appendChild(box);
  }
}
