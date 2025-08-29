import { Options } from "./components/options.js";

export class Application {
  constructor(automobili) {
    this.automobili = automobili;
  }

  draw(container) {
    let leviDiv = document.createElement("div");
    leviDiv.classList.add("leviDiv");

    let desniDiv = document.createElement("div");
    desniDiv.classList.add("desniDiv");

    let opcije = new Options(this.automobili);
    opcije.draw(leviDiv);

    this.automobili.forEach((auto) => {
      auto.draw(desniDiv);
    });

    container.append(leviDiv, desniDiv);
  }
}
