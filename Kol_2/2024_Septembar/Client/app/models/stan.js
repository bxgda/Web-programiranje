import { fetchUkupnoZaduzenje } from "../utils.js";

export class Stan {
  constructor(id, vlasnik, povrsina, brojClanova, racuni) {
    this.id = id;
    this.vlasnik = vlasnik;
    this.povrsina = povrsina;
    this.brojClanova = brojClanova;
    this.racuni = racuni;
  }

  // ovo nije dobro sto se u modelu radi crtanje ali ok tad nismo znali bolje a i realno
  // oni na casovima i pripremama nisu nista bolji

  draw(container) {
    const polja = [
      {
        label: "Broj stana:",
        value: this.id,
        classValue: "lblBrStanaValue",
      },
      {
        label: "Ime vlasnika:",
        value: this.vlasnik,
        classValue: "lblImeVlasnikValue",
      },
      {
        label: "Površina (m2):",
        value: this.povrsina,
        classValue: "lblPovrsinaValue",
      },
      {
        label: "Broj članova:",
        value: this.brojClanova,
        classValue: "lblBrojClanovaValue",
      },
    ];

    polja.forEach((polje) => {
      let lbl = document.createElement("label");
      lbl.innerHTML = polje.label;

      let lblValue = document.createElement("label");
      lblValue.innerHTML = polje.value;
      lblValue.classList.add(polje.classValue);

      container.append(lbl, lblValue);
    });

    // dugme izracunaj ukupno zaduzenje
    const btnIzracunaj = document.createElement("input");
    btnIzracunaj.classList.add("btnIzracunaj");
    btnIzracunaj.classList.add("btnSubmit");
    btnIzracunaj.type = "button";
    btnIzracunaj.value = "Izračunaj ukupno zaduženje";
    btnIzracunaj.onclick = this.handleIzracunaj;
    container.appendChild(btnIzracunaj);
  }

  handleIzracunaj = async () => {
    const zaduzenje = await fetchUkupnoZaduzenje(this.id);
    alert(zaduzenje);
  };
}
