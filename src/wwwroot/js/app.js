// ═══════════════════════════════════════════════════════════════
// NINX ERP — JavaScript Utilities
// ═══════════════════════════════════════════════════════════════

window.NinxApp = {

    /**
     * Foca em um elemento pelo seletor CSS
     */
    focusElement: function (selector) {
        const el = document.querySelector(selector);
        if (el) el.focus();
    },

    /**
     * Exibe uma notificação toast temporária
     */
    showToast: function (message, type = 'info', duration = 3000) {
        const existing = document.querySelector('.ninx-toast');
        if (existing) existing.remove();

        const toast = document.createElement('div');
        toast.className = `ninx-toast ninx-toast-${type}`;
        toast.textContent = message;

        const styles = {
            position: 'fixed',
            bottom: '24px',
            right: '24px',
            padding: '12px 20px',
            borderRadius: '10px',
            fontSize: '14px',
            fontWeight: '500',
            fontFamily: 'Inter, sans-serif',
            zIndex: '9999',
            animation: 'toast-in 0.3s ease',
            boxShadow: '0 8px 24px rgba(0,0,0,0.4)',
        };

        const colors = {
            info:    { bg: 'rgba(26,158,154,0.15)', border: 'rgba(26,158,154,0.3)', color: '#4DD9D0' },
            success: { bg: 'rgba(62,201,122,0.15)', border: 'rgba(62,201,122,0.3)', color: '#5EDBA0' },
            error:   { bg: 'rgba(224,90,106,0.15)', border: 'rgba(224,90,106,0.3)', color: '#F08090' },
            warning: { bg: 'rgba(245,166,35,0.15)', border: 'rgba(245,166,35,0.3)', color: '#F5C842' },
        };

        const c = colors[type] || colors.info;
        Object.assign(toast.style, styles, {
            background: c.bg,
            border: `1px solid ${c.border}`,
            color: c.color,
        });

        document.body.appendChild(toast);

        setTimeout(() => {
            toast.style.animation = 'toast-out 0.3s ease forwards';
            setTimeout(() => toast.remove(), 300);
        }, duration);
    },

    /**
     * Retorna a versão do sistema operacional
     */
    getPlatformInfo: function () {
        return {
            userAgent: navigator.userAgent,
            platform: navigator.platform,
            language: navigator.language,
        };
    }
};

// Injetar keyframes de animação do toast
const style = document.createElement('style');
style.textContent = `
    @keyframes toast-in {
        from { opacity: 0; transform: translateY(10px); }
        to   { opacity: 1; transform: translateY(0); }
    }
    @keyframes toast-out {
        from { opacity: 1; transform: translateY(0); }
        to   { opacity: 0; transform: translateY(10px); }
    }
`;
document.head.appendChild(style);
