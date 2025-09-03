import { fetchStanById } from "./utils.js";

export class Application {
  constructor(stanoviIdsFetched) {
    this.stanoviIds = stanoviIdsFetched;
  }

  draw(container) {
    // Leva strana
    const leftContainer = document.createElement("div");
    leftContainer.className = "leftContainer";
    this.drawLeft(leftContainer);

    // Desna strana
    const rightContainer = document.createElement("div");
    rightContainer.className = "rightContainer";

    container.appendChild(leftContainer);
    container.appendChild(rightContainer);
  }

  drawLeft(container) {
    // Gornji box
    const boxGornji = document.createElement("div");
    boxGornji.classList.add("box");

    // labela
    const lblBirajStan = document.createElement("label");
    lblBirajStan.innerHTML = "Biraj stan:";
    boxGornji.appendChild(lblBirajStan);

    // select (dropdown)
    const selectStan = document.createElement("select");
    selectStan.className = "select";
    this.stanoviIds.forEach((idStana) => {
      const option = document.createElement("option");
      option.value = idStana;
      option.textContent = idStana;
      selectStan.appendChild(option);
    });
    boxGornji.appendChild(selectStan);

    // dugme
    const btnPrikazInf = document.createElement("input");
    btnPrikazInf.type = "button";
    btnPrikazInf.value = "Prikaz informacija";
    btnPrikazInf.className = "btnSubmit";
    btnPrikazInf.onclick = this.handlePrikazInformacija;
    boxGornji.appendChild(btnPrikazInf);

    const boxDonji = document.createElement("div");
    boxDonji.classList.add("boxDonji");

    container.appendChild(boxGornji);
    container.appendChild(boxDonji);
  }

  drawRight(container) {
    this.stan.racuni.forEach((racun) => {
      racun.draw(container);
    });
  }

  handlePrikazInformacija = async () => {
    // Izvuci id iz select-a
    const select = document.querySelector(".select");
    this.stan = await fetchStanById(select.value);

    // Pribavi container-e
    const leftContainer = document.querySelector(".leftContainer");
    const rightContainer = document.querySelector(".rightContainer");
    const boxDonji = document.querySelector(".boxDonji");

    // Ocisti prethodne
    boxDonji.replaceChildren();
    rightContainer.replaceChildren();

    // Crtaj dole i desno
    this.drawBottomBox(leftContainer, boxDonji);
    this.drawRight(rightContainer);
  };

  drawBottomBox(container, boxDonji) {
    boxDonji.classList.add("box");
    this.stan.draw(boxDonji);
    container.appendChild(boxDonji);
  }
}
