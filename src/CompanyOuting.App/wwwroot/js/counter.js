window.animateCounters = function (targets) {
    const duration = 1500;
    const start = performance.now();

    function easeOutCubic(t) {
        return 1 - Math.pow(1 - t, 3);
    }

    function tick(now) {
        const elapsed = now - start;
        const progress = Math.min(elapsed / duration, 1);
        const eased = easeOutCubic(progress);

        targets.forEach(function (t) {
            const el = document.getElementById(t.id);
            if (el) {
                el.textContent = Math.round(t.value * eased).toString();
            }
        });

        if (progress < 1) {
            requestAnimationFrame(tick);
        } else {
            // Ensure exact final values
            targets.forEach(function (t) {
                const el = document.getElementById(t.id);
                if (el) el.textContent = t.value.toString();
            });
        }
    }

    requestAnimationFrame(tick);
};
