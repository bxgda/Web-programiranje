import { Opcije } from "./components/Options.js";
import { Fabrika } from "./models/fabrika.js";

export class Application {
  constructor(fabrika) {
    this.fabrika = fabrika;
  }

  draw(container) {
    let appDiv = document.createElement("div");
    appDiv.classList.add("appDiv");

    let fabrikaDiv = document.createElement("div");
    fabrikaDiv.classList.add("fabrikaDiv");

    let optionsDiv = document.createElement("div");
    optionsDiv.classList.add("optionsDiv");

    appDiv.append(fabrikaDiv, optionsDiv);
    container.appendChild(appDiv);

    let opt = new Opcije(
      this.fabrika.listaSilosa,
      this.fabrika.fabrikaID,
      this.fabrika.naziv
    );
    opt.draw(optionsDiv);

    let fab = new Fabrika(
      this.fabrika.fabrikaID,
      this.fabrika.naziv,
      this.fabrika.listaSilosa
    );
    fab.draw(fabrikaDiv);
  }
}
