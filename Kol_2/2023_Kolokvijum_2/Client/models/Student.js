export class Student {
  constructor(id, indeks, imePrezime, ocene) {
    this.id = id;
    this.indeks = indeks;
    this.imePrezime = imePrezime;
    this.ocene = ocene;
  }

  dodajOcenu(ocena) {
    this.ocene.push(ocena);
  }
}
