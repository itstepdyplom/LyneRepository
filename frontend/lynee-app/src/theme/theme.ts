import { createTheme } from '@mui/material/styles';

const colors = {
  primary: {
    main: '#000000',
    light: '#333333',
    dark: '#000000',
    contrastText: '#FFFFFF',
  },
  secondary: {
    main: '#FFFFFF', 
    light: '#F5F5F5',
    dark: '#E0E0E0',
    contrastText: '#000000',
  },
  accent: {
    main: '#D4A574',
    light: '#E6C49A',
    dark: '#B8935E',
    contrastText: '#000000',
  },
  background: {
    default: '#FFFFFF',
    paper: '#FFFFFF',
    dark: '#0A0A0A',
  },
  text: {
    primary: '#000000',
    secondary: '#666666',
    disabled: '#999999',
  },
  divider: '#E0E0E0',
  error: {
    main: '#FF4444',
    light: '#FF6B6B',
    dark: '#CC0000',
    contrastText: '#FFFFFF',
  },
  warning: {
    main: '#FFA726',
    light: '#FFB74D',
    dark: '#F57C00',
    contrastText: '#000000',
  },
  success: {
    main: '#66BB6A',
    light: '#81C784',
    dark: '#4CAF50',
    contrastText: '#FFFFFF',
  },
};

export const theme = createTheme({
  palette: {
    mode: 'light',
    primary: colors.primary,
    secondary: colors.secondary,
    background: colors.background,
    text: colors.text,
    divider: colors.divider,
    error: colors.error,
    warning: colors.warning,
    success: colors.success,
  },
  typography: {
    fontFamily: '"Inter", "Helvetica", "Arial", sans-serif',
    h1: {
      fontSize: '3.5rem',
      fontWeight: 300,
      letterSpacing: '-0.02em',
      lineHeight: 1.2,
    },
    h2: {
      fontSize: '2.5rem',
      fontWeight: 300,
      letterSpacing: '-0.01em',
      lineHeight: 1.3,
    },
    h3: {
      fontSize: '2rem',
      fontWeight: 400,
      letterSpacing: 0,
      lineHeight: 1.4,
    },
    h4: {
      fontSize: '1.5rem',
      fontWeight: 400,
      letterSpacing: 0,
      lineHeight: 1.4,
    },
    h5: {
      fontSize: '1.25rem',
      fontWeight: 500,
      letterSpacing: 0,
      lineHeight: 1.4,
    },
    h6: {
      fontSize: '1rem',
      fontWeight: 600,
      letterSpacing: '0.02em',
      lineHeight: 1.4,
      textTransform: 'uppercase',
    },
    subtitle1: {
      fontSize: '1.125rem',
      fontWeight: 400,
      letterSpacing: 0,
      lineHeight: 1.5,
    },
    subtitle2: {
      fontSize: '0.875rem',
      fontWeight: 500,
      letterSpacing: '0.02em',
      lineHeight: 1.4,
      textTransform: 'uppercase',
    },
    body1: {
      fontSize: '1rem',
      fontWeight: 400,
      letterSpacing: 0,
      lineHeight: 1.6,
    },
    body2: {
      fontSize: '0.875rem',
      fontWeight: 400,
      letterSpacing: 0,
      lineHeight: 1.5,
    },
    button: {
      fontSize: '0.875rem',
      fontWeight: 500,
      letterSpacing: '0.05em',
      textTransform: 'uppercase',
    },
    caption: {
      fontSize: '0.75rem',
      fontWeight: 400,
      letterSpacing: '0.03em',
      lineHeight: 1.4,
    },
  },
  spacing: 8,
  shape: {
    borderRadius: 0, // Sharp corners for luxury feel
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 0,
          padding: '12px 32px',
          fontSize: '0.875rem',
          fontWeight: 500,
          letterSpacing: '0.1em',
          textTransform: 'uppercase',
          transition: 'all 0.3s ease',
          '&:hover': {
            transform: 'translateY(-1px)',
            boxShadow: '0 4px 12px rgba(0, 0, 0, 0.15)',
          },
        },
        contained: {
          backgroundColor: colors.primary.main,
          color: colors.primary.contrastText,
          boxShadow: 'none',
          '&:hover': {
            backgroundColor: colors.primary.light,
            boxShadow: '0 4px 12px rgba(0, 0, 0, 0.25)',
          },
        },
        outlined: {
          borderColor: colors.primary.main,
          color: colors.primary.main,
          borderWidth: '1px',
          '&:hover': {
            borderColor: colors.primary.main,
            backgroundColor: 'rgba(0, 0, 0, 0.04)',
          },
        },
        text: {
          color: colors.primary.main,
          '&:hover': {
            backgroundColor: 'rgba(0, 0, 0, 0.04)',
          },
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            borderRadius: 0,
            '& fieldset': {
              borderColor: colors.divider,
            },
            '&:hover fieldset': {
              borderColor: colors.primary.main,
            },
            '&.Mui-focused fieldset': {
              borderColor: colors.primary.main,
              borderWidth: '1px',
            },
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 0,
          boxShadow: 'none',
          border: `1px solid ${colors.divider}`,
          transition: 'all 0.3s ease',
          '&:hover': {
            transform: 'translateY(-2px)',
            boxShadow: '0 8px 24px rgba(0, 0, 0, 0.1)',
          },
        },
      },
    },
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundColor: colors.background.default,
          color: colors.text.primary,
          boxShadow: 'none',
          borderBottom: `1px solid ${colors.divider}`,
        },
      },
    },
    MuiDrawer: {
      styleOverrides: {
        paper: {
          borderRadius: 0,
          border: 'none',
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        root: {
          borderRadius: 0,
          fontSize: '0.75rem',
          fontWeight: 500,
          letterSpacing: '0.05em',
          textTransform: 'uppercase',
        },
      },
    },
  },
});

export const darkTheme = createTheme({
  ...theme,
  palette: {
    mode: 'dark',
    primary: {
      main: '#FFFFFF',
      light: '#F5F5F5',
      dark: '#E0E0E0',
      contrastText: '#000000',
    },
    secondary: {
      main: '#000000',
      light: '#333333',
      dark: '#000000',
      contrastText: '#FFFFFF',
    },
    background: {
      default: '#0A0A0A',
      paper: '#1A1A1A',
    },
    text: {
      primary: '#FFFFFF',
      secondary: '#B0B0B0',
      disabled: '#666666',
    },
    divider: '#333333',
  },
}); 