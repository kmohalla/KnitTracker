(function () {
  function getStored() {
    try { return localStorage.getItem('theme'); } catch (e) { return null; }
  }
  function getPreferred() {
    return getStored() ||
      (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
  }
  function apply(theme) {
    document.documentElement.setAttribute('data-bs-theme', theme);
    // keep the Settings page radio buttons in sync
    document.querySelectorAll('input[name="theme"]').forEach(function (r) {
      r.checked = (r.value === theme);
    });
  }
  function setTheme(theme) {
    try { localStorage.setItem('theme', theme); } catch (e) { }
    apply(theme);
  }

  // Runs in <head>, before the page draws, so there's no white flash
  apply(getPreferred());

  document.addEventListener('DOMContentLoaded', function () {
    apply(getPreferred());

    // Settings page radio buttons
    document.querySelectorAll('input[name="theme"]').forEach(function (r) {
      r.addEventListener('change', function () { setTheme(r.value); });
    });

    // Navbar quick-toggle button
    var toggle = document.getElementById('themeToggle');
    if (toggle) {
      toggle.addEventListener('click', function () {
        var current = document.documentElement.getAttribute('data-bs-theme');
        setTheme(current === 'dark' ? 'light' : 'dark');
      });
    }
  });
})();