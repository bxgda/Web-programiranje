import { prebrojJedinstvene } from "../utils.js";

export class SobaStatus {
  constructor(soba, clanoviSobe) {
    this.soba = soba;
    this.clanoviSobe = clanoviSobe;
  }

  draw(container) {
    const sobaDiv = document.createElement("div");
    sobaDiv.classList.add("sobaDiv");

    const imeSobeDiv = document.createElement("div");
    imeSobeDiv.classList.add("imeSobeDiv");

    const lblDiv = document.createElement("div");
    lblDiv.classList.add("lblDiv");

    const clanoviDiv = document.createElement("div");
    clanoviDiv.classList.add("clanoviDiv", `clanoviDiv-${this.soba.naziv}`);

    const dugmeDiv = document.createElement("div");
    dugmeDiv.classList.add("dugmeDiv");

    const lblImeSobe = document.createElement("label");
    lblImeSobe.innerHTML = this.soba.naziv;
    imeSobeDiv.append(lblImeSobe);

    const lblClanovi = document.createElement("label");
    lblClanovi.innerHTML = "Članovi:";
    lblDiv.append(lblClanovi);

    this.clanoviSobe.forEach((clan) => {
      const lblClan = document.createElement("label");
      lblClan.innerHTML = `- ${clan.nadimak} (${clan.korisnickoIme})`;
      lblClan.style.color = clan.boja;
      clanoviDiv.append(lblClan);
    });

    const dugme = document.createElement("button");
    dugme.classList.add("dugmeSoba");
    dugmeDiv.append(dugme);
    dugme.innerHTML = "Prebroj jedinstvene";
    dugme.onclick = this.jedinstveni;

    sobaDiv.append(imeSobeDiv, lblDiv, clanoviDiv, dugmeDiv);
    container.append(sobaDiv);
  }

  jedinstveni = async () => {
    const odgovor = await prebrojJedinstvene(this.soba.id);
    alert(odgovor);
  };
}
