import { iznajmiAuto } from "../utils.js";

export class Automobil {
  constructor(
    automobilID,
    model,
    predjenaKilometraza,
    godiste,
    cenaPoDanu,
    iznajmljen
  ) {
    this.automobilID = automobilID;
    this.model = model;
    this.predjenaKilometraza = predjenaKilometraza;
    this.godiste = godiste;
    this.cenaPoDanu = cenaPoDanu;
    this.iznajmljen = iznajmljen;
    this.autoDiv;
  }
}
