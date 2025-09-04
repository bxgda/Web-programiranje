import {
  dodajMaterijal,
  kupiMaterijal,
  materijalUNajvecojKolicini,
  pronadjiMaterijal,
} from "../utils.js";

export class StovaristeCard {
  constructor(stovariste) {
    this.stovariste = stovariste;

    this.fields = [
      {
        label: "Naziv:",
        type: "text",
        class: `input-naziv-${this.stovariste.id}`,
      },
      {
        label: "Datum:",
        type: "date",
        class: `input-date-${this.stovariste.id}`,
      },
      {
        label: "Količina:",
        type: "number",
        class: `input-kolicina-${this.stovariste.id}`,
      },
      {
        label: "Cena:",
        type: "number",
        class: `input-cena-${this.stovariste.id}`,
      },
    ];
  }

  draw(container) {
    const box = document.createElement("div");
    box.classList.add("stovariste", `stovariste-${this.stovariste.id}`);

    const naslov = document.createElement("h2");
    naslov.innerText = `${this.stovariste.naziv} (${this.stovariste.adresa})`;
    naslov.style.textAlign = "center";
    box.append(naslov);

    const formsContainer = document.createElement("div");
    formsContainer.className = "forms-container";

    this.drawDodajMaterijal(formsContainer);
    this.drawPronadjiKupi(formsContainer);
    this.drawMaterijalUNajvecojKolicini(formsContainer);

    box.append(formsContainer);

    container.append(box);
  }

  drawMaterijalUNajvecojKolicini(container) {
    const box = document.createElement("div");
    box.className = "box";

    const naslov = document.createElement("h3");
    naslov.innerText = "Materijal u najvećoj količini";
    naslov.style.textAlign = "center";
    box.append(naslov);

    const maxMatBox = document.createElement("div");
    maxMatBox.className = `maxMat-${this.stovariste.id}`;
    this.fillMaxMat(maxMatBox);

    box.append(maxMatBox);

    container.append(box);
  }

  async fillMaxMat(container) {
    container.replaceChildren();

    const mat = document.createElement("span");
    const matUNajvKol = await materijalUNajvecojKolicini(this.stovariste.id);
    if (matUNajvKol.error) {
      mat.innerText = matUNajvKol.error;
    }
    const materijal = matUNajvKol.data.naziv;
    const datum = matUNajvKol.data.datum;

    mat.innerText = `${materijal} (od ${datum})`;
    mat.style.color = "blue";

    container.append(mat);
  }

  drawPronadjiKupi(container) {
    const box = document.createElement("div");
    box.className = "box";

    const naslov = document.createElement("h3");
    naslov.innerText = "Kupovina materijala";
    naslov.style.textAlign = "center";
    box.append(naslov);

    // Search field
    const pretragaDiv = document.createElement("div");
    pretragaDiv.className = "pretraga-div";

    const searchInput = document.createElement("input");
    searchInput.type = "text";
    searchInput.placeholder = "Naziv materijala...";
    searchInput.className = `input-search-${this.stovariste.id}`;

    const btnSearch = document.createElement("button");
    btnSearch.onclick = this.handleSearch;
    btnSearch.innerText = "Nađi";

    pretragaDiv.append(searchInput, btnSearch);

    // Select
    const select = document.createElement("select");
    select.className = `select-${this.stovariste.id}`;
    select.style.margin = "10px 0px";

    // Kupovina
    const kupovinaDiv = document.createElement("div");
    kupovinaDiv.className = "pretraga-div";

    const kolicinaInput = document.createElement("input");
    kolicinaInput.type = "number";
    kolicinaInput.placeholder = "Količina [kg]";
    kolicinaInput.className = `input-kupi-${this.stovariste.id}`;

    const btnKupi = document.createElement("button");
    btnKupi.onclick = this.handleKupi;
    btnKupi.innerText = "Kupi";

    kupovinaDiv.append(kolicinaInput, btnKupi);

    box.append(pretragaDiv, select, kupovinaDiv);

    container.append(box);
  }

  handleKupi = async () => {
    const selectValue = document.querySelector(
      `.select-${this.stovariste.id}`
    ).value;

    const kolicinaInput = document.querySelector(
      `.input-kupi-${this.stovariste.id}`
    );

    if (selectValue === "" || kolicinaInput.value === "") {
      alert("Unesite kolicinu i izaberite materijal");
      return;
    }

    const msg = await kupiMaterijal(
      this.stovariste.id,
      selectValue,
      kolicinaInput.value
    );

    this.handleSearch();

    alert(msg);
  };

  handleSearch = async () => {
    const searchEl = document.querySelector(
      `.input-search-${this.stovariste.id}`
    );

    const { error, data } = await pronadjiMaterijal(
      this.stovariste.id,
      searchEl.value
    );

    if (error) {
      alert(error);
      return;
    }

    // Popuni select
    const select = document.querySelector(`.select-${this.stovariste.id}`);
    select.replaceChildren();
    data.forEach((m) => {
      const option = document.createElement("option");
      option.value = m.id;
      option.innerText = `${m.naziv} | Cena: ${m.cena} | Dostupno: ${m.kolicina}`;
      select.append(option);
    });
    console.log(data);
  };

  drawDodajMaterijal(container) {
    const box = document.createElement("div");
    box.className = "box";

    const naslov = document.createElement("h3");
    naslov.innerText = "Dodavanje materijala";
    naslov.style.textAlign = "center";

    const formDodavanje = document.createElement("div");
    formDodavanje.className = "form-dodavanje";

    this.fields.forEach((f) => {
      const lbl = document.createElement("label");
      lbl.innerText = f.label;
      const input = document.createElement("input");
      input.type = f.type;
      input.className = f.class;
      formDodavanje.append(lbl, input);
    });

    const btnDodaj = document.createElement("button");
    btnDodaj.className = "btn-dodaj";
    btnDodaj.innerText = "Dodaj";
    btnDodaj.onclick = this.handleDodajMaterijal;

    formDodavanje.append(btnDodaj);

    box.append(naslov, formDodavanje);

    container.append(box);
  }

  handleDodajMaterijal = async () => {
    const nazivEl = document.querySelector(
      `.input-naziv-${this.stovariste.id}`
    );
    const datumEl = document.querySelector(`.input-date-${this.stovariste.id}`);
    const kolEl = document.querySelector(
      `.input-kolicina-${this.stovariste.id}`
    );
    const cenaEl = document.querySelector(`.input-cena-${this.stovariste.id}`);

    const { error, data } = await dodajMaterijal(
      this.stovariste.id,
      kolEl.value,
      cenaEl.value,
      nazivEl.value,
      datumEl.value
    );

    if (error != null) {
      alert(error);
      return;
    }

    const cont = document.querySelector(`.maxMat-${this.stovariste.id}`);
    this.fillMaxMat(cont);

    nazivEl.value = "";
    datumEl.value = "";
    kolEl.value = null;
    cenaEl.value = null;

    alert("Uspesno dodat materijal");
  };
}
