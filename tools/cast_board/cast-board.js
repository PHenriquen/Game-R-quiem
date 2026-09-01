const grid = document.querySelector("#castGrid");
const buttons = [...document.querySelectorAll(".mode")];

function setMode(mode) {
  grid.classList.toggle("silhouette", mode === "silhouette");
  grid.classList.toggle("sprite", mode === "sprite");

  buttons.forEach((button) => {
    const active = button.dataset.mode === mode;
    button.classList.toggle("active", active);
    button.setAttribute("aria-pressed", String(active));
  });
}

buttons.forEach((button) => {
  button.addEventListener("click", () => setMode(button.dataset.mode));
});
