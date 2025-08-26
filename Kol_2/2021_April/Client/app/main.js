import { Application } from './application.js';

const meraciFetched = await fetchMeraci();

const app = new Application(meraciFetched);
app.draw(document.body);

async function fetchMeraci() {
  try {
    const response = await fetch('https://localhost:5001/Merac/PreuzmiSveMerace');
    const data = await response.json();

    return data;
  } catch (error) {
    console.error(error);
  }
}
