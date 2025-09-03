import { RibaURezervoaru } from "../models/ribaURezervoaru.js";
import { dodajRibuURezervoar, izmeni } from "../utils.js";

export class RezervoarInfo {
  constructor(rezervoar, ribe) {
    this.rezervoar = rezervoar;
    this.ribe = ribe; // sve ribe u bazi za dropdown

    // sluzi za popunjavanje vrhova kolona i pristupanje
    // razlicitim atributima klase RibaURezervoaru za razlicite kolone
    this.labeleKolona = [
      {
        header: "Vrsta",
        imeAtributa: "nazivRibe",
      },
      {
        header: "Broj komada",
        imeAtributa: "brojKomada",
      },
      {
        header: "Masa",
        imeAtributa: "masa",
      },
      {
        header: "", // u forEach kad naletimo na ovo znamo da treba tu dugme da se pravi
        imeAtributa: "",
      },
    ];

    this.izmenaId = -1;
  }

  draw(container) {
    // naslov i kostur i pozivanje funkcija za levi deo i desni

    const ceoInfo = document.createElement("div");
    ceoInfo.classList.add("ceoInfo");

    const imeRezDiv = document.createElement("div");
    imeRezDiv.classList.add("imeRezDiv");

    const imeRez = document.createElement("label");
    imeRez.innerHTML = `${this.rezervoar.sifra}(${this.rezervoar.kapacitet})`;

    //ceo div za donji deo (donji u odnosu na naslov)
    const donjiDeoRezDiv = document.createElement("div");
    donjiDeoRezDiv.classList.add("donjiDeoRezDiv");

    imeRezDiv.append(imeRez);

    this.drawLeviDeo(donjiDeoRezDiv);
    this.drawDesniDeo(donjiDeoRezDiv);

    ceoInfo.append(imeRezDiv, donjiDeoRezDiv);

    container.append(ceoInfo);
  }

  drawLeviDeo(container) {
    // pravimo donji levi div i pozivamo funkcije za crtanje tabele i edit-a

    const tabelaIEditDiv = document.createElement("div");
    tabelaIEditDiv.classList.add("tabelaIEditDiv");

    // tabela
    this.drawTabela(tabelaIEditDiv);

    // izemena forma
    this.drawIzmenaForme(tabelaIEditDiv);

    container.append(tabelaIEditDiv);
  }

  drawTabela(container) {
    const tabela = document.createElement("table");
    tabela.classList.add(`tabela-${this.rezervoar.id}`);

    const tr = document.createElement("tr"); // skroz gornji red

    this.labeleKolona.forEach((kolona) => {
      const th = document.createElement("th");
      th.innerText = kolona.header;
      tr.append(th);
    });

    tabela.append(tr);

    this.rezervoar.ribeURezervoaru.forEach((riba) => {
      this.dodajRedUTabelu(tabela, riba);
    });

    container.append(tabela);
  }

  dodajRedUTabelu(tabela, riba) {
    const tr = document.createElement("tr"); // ceo red
    tr.classList.add(`red-${riba.idSpoja}`);

    this.labeleKolona.forEach((kolona) => {
      const td = document.createElement("td"); // jedna celija

      if (kolona.imeAtributa === "") {
        // u ovom slucaju crtamo dugme

        const dugmeIzmeni = document.createElement("button");
        dugmeIzmeni.innerHTML = "Izmeni";
        dugmeIzmeni.classList.add(`dugmeIzmeni-${riba.idSpoja}`);
        dugmeIzmeni.onclick = () => this.pokaziFormuIzmene(riba);
        td.append(dugmeIzmeni);
      } else {
        // inace pisemo tekst koji treba

        td.innerText = riba[kolona.imeAtributa]; // pristupamo vrednosti atributa ribe
      }

      tr.append(td);
    });

    tabela.append(tr);
  }

  drawIzmenaForme(container) {
    // ideja je da iscrtamo to ali da ne pokazemo pa kad se klikne na dugme da se pokaze i izmeni labela za naslov

    const formaIzmeneDiv = document.createElement("div");
    formaIzmeneDiv.classList.add(
      "formaIzmeneDiv",
      `formaIzmeneDiv-${this.rezervoar.id}`
    );

    const izmenaNaslovDiv = document.createElement("div");
    izmenaNaslovDiv.classList.add("izmenaNaslovDiv");

    const naslov = document.createElement("label");
    naslov.innerHTML = "Izmena";
    naslov.classList.add(`izmenaNaslov-${this.rezervoar.id}`);

    izmenaNaslovDiv.append(naslov);
    formaIzmeneDiv.append(izmenaNaslovDiv);

    const polja = [
      {
        label: "Br komada:",
        class: `brKomadaIzmenaInput-${this.rezervoar.id}`,
      },
      {
        label: "Masa:",
        class: `masaIzmenaInput-${this.rezervoar.id}`,
      },
    ];

    polja.forEach((polje) => {
      const box = document.createElement("div");
      box.classList.add("boxIzmena");

      const label = document.createElement("label");
      label.innerHTML = polje.label;

      const input = document.createElement("input");
      input.classList.add(polje.class);
      input.type = "number";

      box.append(label, input);
      formaIzmeneDiv.append(box);
    });

    const dugmePotvrdiIzmene = document.createElement("button");
    dugmePotvrdiIzmene.innerHTML = "Sačuvaj izmene";
    dugmePotvrdiIzmene.onclick = this.potvrdiIzmene;

    formaIzmeneDiv.append(dugmePotvrdiIzmene);

    formaIzmeneDiv.style.display = "none";
    container.append(formaIzmeneDiv);
  }

  drawDesniDeo(container) {
    // desni deo za dodavanje ribe u rezervoar

    const dodajRibuURezDiv = document.createElement("div");
    dodajRibuURezDiv.classList.add(
      "dodajRibuURezDiv",
      `dodajRibuURezDiv-${this.rezervoar.id}`
    );

    const naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovDodajRibuURez");

    const lblNaslov = document.createElement("label");
    lblNaslov.innerHTML = "Dodavanje u rezervoar";

    naslovDiv.append(lblNaslov);

    dodajRibuURezDiv.append(naslovDiv);

    // dropdown
    const selectRibaDiv = document.createElement("div");
    selectRibaDiv.classList.add("dodavanjeBox");

    const selectLabel = document.createElement("label");
    selectLabel.innerHTML = "Riba:";

    const selectRiba = document.createElement("select");
    this.ribe.forEach((riba) => {
      const opt = document.createElement("option");
      opt.value = riba.id;
      opt.innerHTML = riba.vrsta;
      selectRiba.append(opt);
    });

    selectRibaDiv.append(selectLabel, selectRiba);

    dodajRibuURezDiv.append(selectRibaDiv);

    const polja = [
      {
        label: "Br kom:",
        input: "text",
        class: `brKomInput-${this.rezervoar.id}`,
      },
      {
        label: "Masa:",
        input: "number",
        class: `masaInput-${this.rezervoar.id}`,
      },
      {
        label: "Datum:",
        input: "date",
        class: `datumInput-${this.rezervoar.id}`,
      },
    ];

    polja.forEach((polje) => {
      const box = document.createElement("div");
      box.classList.add("boxDodajURez");

      const label = document.createElement("label");
      label.innerHTML = polje.label;

      const input = document.createElement("input");
      input.classList.add(polje.class);
      input.type = polje.input;

      if (polje.input === "date") {
        const now = new Date();
        input.value = now.toISOString().slice(0, 10);
      }

      box.append(label, input);
      dodajRibuURezDiv.append(box);
    });

    const dugmeDodajURez = document.createElement("button");
    dugmeDodajURez.innerHTML = "Dodaj";
    dugmeDodajURez.onclick = this.dodajURezervoar;

    dodajRibuURezDiv.append(dugmeDodajURez);

    container.append(dodajRibuURezDiv);
  }

  // funkcionalnosti dugmica

  pokaziFormuIzmene = (riba) => {
    const title = document.querySelector(`.izmenaNaslov-${this.rezervoar.id}`);
    title.innerHTML = `Izmena: ${riba.nazivRibe} (${riba.brojKomada}, ${riba.masa})`;

    this.izmenaId = riba.idSpoja; // id spoja ribe i rezervoara

    const container = document.querySelector(
      `.formaIzmeneDiv-${this.rezervoar.id}`
    );
    container.style.display = "flex";
  };

  potvrdiIzmene = async () => {
    const brojEl = document.querySelector(
      `.brKomadaIzmenaInput-${this.rezervoar.id}`
    );
    const masaEl = document.querySelector(
      `.masaIzmenaInput-${this.rezervoar.id}`
    );
    const datum = new Date().toISOString();

    const response = await izmeni(
      this.izmenaId,
      brojEl.value,
      masaEl.value,
      datum
    );

    if (!response) {
      return;
    }

    const tr = document.querySelector(`.red-${this.izmenaId}`);
    const tds = tr.querySelectorAll("td");

    if (tds.length >= 3) {
      // drugi i treći <td>
      const tdDrugi = tds[1];
      const tdTreci = tds[2];

      // ažuriraj ih novim vrednostima
      tdDrugi.innerText = brojEl.value;
      tdTreci.innerText = masaEl.value;
    }

    brojEl.value = "";
    masaEl.value = "";

    // sakrivanje forme
    const izmenaForm = document.querySelector(
      `.formaIzmeneDiv-${this.rezervoar.id}`
    );
    izmenaForm.style.display = "none";
    this.izmenaId = -1;
  };

  dodajURezervoar = async () => {
    const container = document.querySelector(
      `.dodajRibuURezDiv-${this.rezervoar.id}`
    );
    if (!container) return;

    const select = container.querySelector("select");
    const ribaId = select?.value;

    const brEl = container.querySelector(`.brKomInput-${this.rezervoar.id}`);
    const masaEl = container.querySelector(`.masaInput-${this.rezervoar.id}`);
    const datumEl = container.querySelector(`.datumInput-${this.rezervoar.id}`);

    const broj = brEl?.value;
    const masa = masaEl?.value;
    const datum = datumEl?.value;

    const { error, data } = await dodajRibuURezervoar(
      this.rezervoar.id,
      ribaId,
      broj,
      datum,
      masa
    );

    if (error) {
      alert(error);
      return;
    } else {
      // Pribavi tabelu
      const table = document.querySelector(`.tabela-${this.rezervoar.id}`);
      const riba = new RibaURezervoaru(
        data.id,
        data.nazivRibe,
        data.brojKomada,
        data.masa
      );

      // Dodaj novi red u tabelu
      this.dodajRedUTabelu(table, riba);
    }
  };
}
