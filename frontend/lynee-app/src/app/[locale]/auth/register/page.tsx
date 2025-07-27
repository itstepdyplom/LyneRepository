import React from 'react';
import { Box, Typography, Container, Paper } from '@mui/material';
import { useTranslations } from 'next-intl';

export default function RegisterPage() {
  const t = useTranslations('Auth');

  return (
    <Container maxWidth="sm">
      <Box
        sx={{
          minHeight: '60vh',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
        }}
      >
        <Paper
          elevation={3}
          sx={{
            p: 4,
            width: '100%',
            textAlign: 'center',
          }}
        >
          <Typography variant="h4" component="h1" gutterBottom>
            {t('register')}
          </Typography>
          <Typography variant="body1" color="text.secondary">
            {t('registerDescription')}
          </Typography>
        </Paper>
      </Box>
    </Container>
  );
} 