export class Chat {
  // ovu klasu ja ne bih ovako nazvao ali u back je tako neko nazvao
  // ovo je u sustini tabela veze N:M koja spaja korisnike i sobe

  constructor(id, korisnickoIme, nadimak, boja) {
    this.id = id;
    this.korisnickoIme = korisnickoIme;
    this.nadimak = nadimak;
    this.boja = boja;
  }
}
