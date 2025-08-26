export class Merac {
  constructor(
    id,
    naziv,
    interval,
    boja,
    granicaOd,
    granicaDo,
    trenutnaVrednost,
    minVrednost,
    maxVrednost,
    prosecnaVrednost
  ) {
    this.id = id;
    this.naziv = naziv;
    this.interval = interval;
    this.boja = boja;
    this.granicaOd = granicaOd;
    this.granicaDo = granicaDo;
    this.trenutnaVrednost = trenutnaVrednost;
    this.minVrednost = minVrednost;
    this.maxVrednost = maxVrednost;
    this.prosecnaVrednost = prosecnaVrednost;
  }

  draw(container) {
    const meracContainer = document.createElement('div');
    meracContainer.classList.add('meracContainer');

    // zaglavlje sa input poljem (gore)
    const zaglavljeContainer = document.createElement('div');
    zaglavljeContainer.classList.add('zaglavljeContainer');
    this.drawZaglavlje(zaglavljeContainer);

    // bar sa lestvicama (dole)
    const barContainer = document.createElement('div');
    barContainer.classList.add('barContainer');
    this.drawBar(barContainer);

    meracContainer.appendChild(zaglavljeContainer);
    meracContainer.appendChild(barContainer);

    container.appendChild(meracContainer);
  }

  drawBar(container) {
    // leva strana
    const crticeContainer = document.createElement('div');
    crticeContainer.classList.add('crticeContainer');

    for (let i = this.granicaDo; i >= this.granicaOd; i -= this.interval) {
      // crtica
      const crtica = document.createElement('div');
      crtica.classList.add('crtica');

      const label = document.createElement('span');
      label.innerHTML = i;
      crtica.appendChild(label);

      crticeContainer.appendChild(crtica);
    }

    // fejkujemo 0
    const prazno = document.createElement('div');
    crticeContainer.appendChild(prazno);

    // desna strana
    const rightBarContainer = document.createElement('div');
    rightBarContainer.classList.add('rightBarContainer');

    const bar = document.createElement('div');
    bar.classList.add('bar');
    bar.classList.add(`bar-${this.id}`);
    bar.style.backgroundColor = this.boja;

    // jbm li ga kako smo ga opravili ni mi ne znamo
    const x = (this.trenutnaVrednost - this.granicaOd + this.interval) * 100;
    const y = this.granicaDo - this.granicaOd + this.interval;
    const h = x / y;

    bar.style.height = `${h}%`;

    rightBarContainer.appendChild(bar);

    container.appendChild(crticeContainer);
    container.appendChild(rightBarContainer);
  }

  drawZaglavlje(container) {
    const naziv = document.createElement('span');
    naziv.innerHTML = this.naziv;

    const vrednostInput = document.createElement('input');
    vrednostInput.type = 'number';
    vrednostInput.value = this.trenutnaVrednost;
    vrednostInput.className = `input-${this.id}`;

    const btnSubmit = document.createElement('input');
    btnSubmit.type = 'button';
    btnSubmit.value = 'Setuj vrednost';
    btnSubmit.onclick = this.handleSubmit;

    const maks = document.createElement('span');
    maks.className = `maks-${this.id}`;
    maks.innerHTML = `Max izmerena vrednost: ${this.maxVrednost}`;

    const min = document.createElement('span');
    min.className = `min-${this.id}`;
    min.innerHTML = `Min izmerena vrednost: ${this.minVrednost}`;

    const prosek = document.createElement('span');
    prosek.className = `prosek-${this.id}`;

    prosek.innerHTML = `Prosečna vrednost: ${this.prosecnaVrednost.toFixed(2)}`;

    container.appendChild(naziv);
    container.appendChild(vrednostInput);
    container.appendChild(btnSubmit);
    container.appendChild(maks);
    container.appendChild(min);
    container.appendChild(prosek);
  }

  handleSubmit = async () => {
    const input = document.querySelector(`.input-${this.id}`);
    const novaVrednost = parseFloat(input.value);

    try {
      const response = await fetch(
        `https://localhost:5001/Merac/IzmeniMerac/${this.id}/${novaVrednost}`,
        {
          method: 'PUT',
        }
      );
      const data = await response.json();

      // Error handling
      if (data.value) {
        alert(data.value);
        return;
      }

      // Change with new values
      this.maxVrednost = data.maksimalnaIzmerenaVrednost;
      this.minVrednost = data.minimalnaIzmerenaVrednost;
      this.prosecnaVrednost = data.prosecnaIzmerenaVrednost;
      this.trenutnaVrednost = novaVrednost;

      // Change UI (rerender on screen)
      const trVrednostEl = document.querySelector(`.input-${this.id}`);
      trVrednostEl.value = novaVrednost;

      const maksEl = document.querySelector(`.maks-${this.id}`);
      maksEl.innerHTML = 'Max izmerena vrednost: ' + this.maxVrednost;

      const minEl = document.querySelector(`.min-${this.id}`);
      minEl.innerHTML = 'Min izmerena vrednost: ' + this.minVrednost;

      const prosekEl = document.querySelector(`.prosek-${this.id}`);
      prosekEl.innerHTML = 'Prosečna vrednost: ' + this.prosecnaVrednost.toFixed(2);

      const barEl = document.querySelector(`.bar-${this.id}`);
      const x = (this.trenutnaVrednost - this.granicaOd + this.interval) * 100;
      const y = this.granicaDo - this.granicaOd + this.interval;
      const h = x / y;

      barEl.style.height = `${h}%`;
    } catch (error) {
      console.error(error);
    }
  };
}
