import React from 'react';
import { Box, Typography, Container, Paper } from '@mui/material';
import { useTranslations } from 'next-intl';

export default function MenPage() {
  const t = useTranslations('Categories');

  return (
    <Container maxWidth="lg">
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
            {t('men')}
          </Typography>
          <Typography variant="body1" color="text.secondary">
            {t('menDescription')}
          </Typography>
        </Paper>
      </Box>
    </Container>
  );
} 