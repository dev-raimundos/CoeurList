const PADDING_X = 8;
const PADDING_Y = 6;

export function updateIndicator(navEl, indicatorEl) {
    if (!navEl || !indicatorEl) {
        return;
    }

    const activeEl = navEl.querySelector('.bottom-nav-item.active');
    if (!activeEl) {
        indicatorEl.style.opacity = '0';
        return;
    }

    const navRect = navEl.getBoundingClientRect();
    const itemRect = activeEl.getBoundingClientRect();

    const x = itemRect.left - navRect.left - PADDING_X;
    const y = itemRect.top - navRect.top - PADDING_Y;

    indicatorEl.style.opacity = '1';
    indicatorEl.style.width = `${itemRect.width + PADDING_X * 2}px`;
    indicatorEl.style.height = `${itemRect.height + PADDING_Y * 2}px`;
    indicatorEl.style.transform = `translate(${x}px, ${y}px)`;
}
