import React from 'react';
import { Box, Typography, Container, Paper } from '@mui/material';
import { useTranslations } from 'next-intl';

export default function WomenPage() {
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
            {t('women')}
          </Typography>
          <Typography variant="body1" color="text.secondary">
            {t('womenDescription')}
          </Typography>
        </Paper>
      </Box>
    </Container>
  );
} 