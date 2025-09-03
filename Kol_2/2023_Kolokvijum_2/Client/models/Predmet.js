export class Predmet {
  constructor(id, naziv, boja = '#000000') {
    this.id = id;
    this.naziv = naziv;
    this.boja = boja;
  }

  setBoja(boja) {
    this.boja = boja;
  }
}
