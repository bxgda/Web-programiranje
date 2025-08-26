export class Draw {
    constructor(factories) {
        this.factories = factories;
        this.container = null;
    }

    DrawApp(host) {
        this.container = document.createElement("div");
        this.container.className = "main-container";
        host.appendChild(this.container);
    
        for(let i = 0; i < this.factories.length; i++) {
            this.DrawFactory(this.container, this.factories[i]);
        }
    }

    DrawFactory(host, fact) {
        let factory = document.createElement("div");
        factory.className = "factory-container";

        let firstContainer = document.createElement("div");
        firstContainer.className = "factory-structure";

        let title = document.createElement("h2");
        title.textContent = fact.naziv;

        firstContainer.appendChild(title);

        let silosContainer = document.createElement("div");
        silosContainer.className = "silos-container";
        this.DrawSilos(silosContainer, fact.listaSilosa);

        firstContainer.appendChild(silosContainer);

        factory.appendChild(firstContainer);

        let datasField = document.createElement("div");
        datasField.className = "datas-field";
        
        let labels = ["Silos:", "Kolicina:"];
        let inputs = ["text", "number"];

        labels.forEach((label, index) => {
            let pomDiv = document.createElement("div");

            let lbl = document.createElement("label");
            lbl.textContent = label;
            pomDiv.appendChild(lbl);

            let inp = document.createElement("input");
            inp.required = true;
            inp.type = inputs[index];
            pomDiv.appendChild(inp);

            datasField.appendChild(pomDiv);
        });

        let sipajBtn = document.createElement("button");
        sipajBtn.textContent = "Sipaj u silos";

        sipajBtn.addEventListener('click', async (event) => {
            let btn = event.currentTarget;
            let vrednosti = btn.parentNode.querySelectorAll("div > input");
            let kompanija = btn.parentNode.parentNode.children[0].children[0].textContent;
            
            const response = await fetch(`http://localhost:5121/IzmeniKolicinu/${encodeURIComponent(kompanija)}/${vrednosti[0].value}/${parseInt(vrednosti[1].value)}`, {
                method: "PUT"
            });
        
            if(response.ok) {
                const list = await (await fetch(`http://localhost:5121/VratiSilose/${encodeURIComponent(kompanija)}`)).json();

                this.DrawSilos(silosContainer, list.silosi);
            }
            else {
                let odgovor = response.text();

                window.alert(odgovor);
            }
        });

        datasField.appendChild(sipajBtn);

        factory.appendChild(datasField);

        host.appendChild(factory);
    }

    DrawSilos(host, silosList) {
        host.innerHTML = "";

        silosList.forEach(silos => {
            let divHolder = document.createElement("div");
            divHolder.className = "div-holder";

            let labelOznaka = document.createElement("label");
            labelOznaka.textContent = silos.oznaka;
            divHolder.appendChild(labelOznaka);

            let labelPopunjenost = document.createElement("label");
            labelPopunjenost.textContent = `${silos.trenutnaKolicina}t/${silos.maxKapacitet}t`;
            divHolder.appendChild(labelPopunjenost);

            let silosDiv = document.createElement("div");
            silosDiv.className = "silos-div";

            let kolicinaDiv = document.createElement("div");
            kolicinaDiv.className = "pravi-silos";
            if(silos.trenutnaKolicina == 0) {
                kolicinaDiv.style.height = "0px";
            }
            else {
                kolicinaDiv.style.height = `${(silos.trenutnaKolicina/silos.maxKapacitet) * 150}px`;
                kolicinaDiv.style.backgroundColor = "#e0e0e0e5";
            }
            
            silosDiv.appendChild(kolicinaDiv);

            divHolder.appendChild(silosDiv);

            host.appendChild(divHolder);
        });
    }
}