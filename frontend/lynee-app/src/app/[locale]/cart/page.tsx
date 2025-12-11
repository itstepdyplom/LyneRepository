'use client';

import React from 'react';
import {
  Box,
  Typography,
  Container,
  Paper,
  Grid,
  IconButton,
  Button,
  Stack,
  Divider,
  TextField,
  Avatar,
} from '@mui/material';
import {
  Add as AddIcon,
  Remove as RemoveIcon,
  Delete as DeleteIcon,
  ShoppingBagOutlined as ShoppingBagIcon,
} from '@mui/icons-material';
import { useTranslations } from 'next-intl';
import { useParams } from 'next/navigation';
import Link from 'next/link';
import { useCartStore } from '../../../stores/cartStore';

export default function CartPage() {
  const t = useTranslations('Cart');
  const params = useParams();
  const locale = params.locale as string;
  
  const {
    items,
    removeItem,
    updateQuantity,
    totalItems,
    totalPrice,
  } = useCartStore();

  const handleQuantityChange = (id: string, newQuantity: number) => {
    if (newQuantity <= 0) {
      removeItem(id);
    } else {
      updateQuantity(id, newQuantity);
    }
  };

  if (items.length === 0) {
    return (
      <Container maxWidth="lg" sx={{ py: 8 }}>
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
            minHeight: '50vh',
            textAlign: 'center',
          }}
        >
          <ShoppingBagIcon
            sx={{
              fontSize: 80,
              color: 'text.secondary',
              mb: 3,
            }}
          />
          <Typography variant="h4" component="h1" gutterBottom>
            {t('emptyTitle')}
          </Typography>
          <Typography variant="body1" color="text.secondary" sx={{ mb: 4, maxWidth: 400 }}>
            {t('emptyDescription')}
          </Typography>
          <Button
            variant="contained"
            component={Link}
            href={`/${locale}`}
            sx={{
              px: 4,
              py: 1.5,
              fontSize: '1rem',
              fontWeight: 500,
              letterSpacing: '0.05em',
            }}
          >
            {t('continueShopping')}
          </Button>
        </Box>
      </Container>
    );
  }

  return (
    <Container maxWidth="lg" sx={{ py: { xs: 4, md: 6 } }}>
      <Typography
        variant="h4"
        component="h1"
        sx={{
          mb: 4,
          fontWeight: 400,
          letterSpacing: '0.1em',
          fontSize: { xs: '1.5rem', md: '2rem' },
        }}
      >
        {t('title')} ({totalItems})
      </Typography>

      <Grid container spacing={4}>
        <Grid item xs={12} md={8}>
          <Paper elevation={0} sx={{ border: '1px solid', borderColor: 'divider' }}>
            {items.map((item, index) => (
              <React.Fragment key={item.id}>
                <Box
                  sx={{
                    display: 'flex',
                    p: { xs: 2, md: 3 },
                    gap: 3,
                    alignItems: 'flex-start',
                  }}
                >
                  <Avatar
                    src={item.image}
                    alt={item.name}
                    variant="square"
                    sx={{
                      width: { xs: 80, md: 120 },
                      height: { xs: 80, md: 120 },
                      backgroundColor: 'grey.200',
                      flexShrink: 0,
                    }}
                  />
                  
                  <Box sx={{ flex: 1, minWidth: 0 }}>
                    <Typography
                      variant="h6"
                      sx={{
                        fontWeight: 500,
                        fontSize: { xs: '1rem', md: '1.1rem' },
                        mb: 1,
                      }}
                    >
                      {item.name}
                    </Typography>
                    
                    <Typography
                      variant="body2"
                      color="text.secondary"
                      sx={{ fontSize: { xs: '0.85rem', md: '0.9rem' }, mb: 2 }}
                    >
                      {t('size')}: {item.size} | {t('color')}: {item.color}
                    </Typography>

                    <Box
                      sx={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        flexWrap: 'wrap',
                        gap: 2,
                      }}
                    >
                      <Typography
                        variant="h6"
                        sx={{
                          fontWeight: 600,
                          color: 'primary.main',
                          fontSize: { xs: '1rem', md: '1.2rem' },
                        }}
                      >
                        ${item.price.toLocaleString()}
                      </Typography>

                      <Stack direction="row" alignItems="center" spacing={1}>
                        <IconButton
                          size="small"
                          onClick={() => handleQuantityChange(item.id, item.quantity - 1)}
                          sx={{
                            border: '1px solid',
                            borderColor: 'divider',
                            width: { xs: 32, md: 36 },
                            height: { xs: 32, md: 36 },
                          }}
                        >
                          <RemoveIcon fontSize="small" />
                        </IconButton>
                        
                        <TextField
                          value={item.quantity}
                          size="small"
                          inputProps={{
                            min: 1,
                            style: {
                              textAlign: 'center',
                              padding: '8px 4px',
                              width: '50px',
                            },
                          }}
                          onChange={(e) => {
                            const newQuantity = parseInt(e.target.value) || 1;
                            handleQuantityChange(item.id, newQuantity);
                          }}
                          sx={{
                            width: 70,
                            '& .MuiOutlinedInput-root': {
                              '& fieldset': {
                                borderColor: 'divider',
                              },
                            },
                          }}
                        />
                        
                        <IconButton
                          size="small"
                          onClick={() => handleQuantityChange(item.id, item.quantity + 1)}
                          sx={{
                            border: '1px solid',
                            borderColor: 'divider',
                            width: { xs: 32, md: 36 },
                            height: { xs: 32, md: 36 },
                          }}
                        >
                          <AddIcon fontSize="small" />
                        </IconButton>
                        
                        <IconButton
                          size="small"
                          onClick={() => removeItem(item.id)}
                          sx={{
                            ml: 1,
                            color: 'error.main',
                          }}
                        >
                          <DeleteIcon />
                        </IconButton>
                      </Stack>
                    </Box>
                  </Box>
                </Box>
                {index < items.length - 1 && <Divider />}
              </React.Fragment>
            ))}
          </Paper>
        </Grid>

        <Grid item xs={12} md={4}>
          <Paper
            elevation={0}
            sx={{
              p: 3,
              border: '1px solid',
              borderColor: 'divider',
              position: { md: 'sticky' },
              top: { md: 20 },
            }}
          >
            <Typography
              variant="h6"
              sx={{
                fontWeight: 400,
                letterSpacing: '0.05em',
                mb: 3,
                fontSize: { xs: '1rem', md: '1.1rem' },
              }}
            >
              {t('orderSummary')}
            </Typography>

            <Box
              sx={{
                display: 'flex',
                justifyContent: 'space-between',
                mb: 2,
              }}
            >
              <Typography variant="body2" color="text.secondary">
                {t('subtotal')}
              </Typography>
              <Typography variant="body2">
                ${totalPrice.toLocaleString()}
              </Typography>
            </Box>

            <Box
              sx={{
                display: 'flex',
                justifyContent: 'space-between',
                mb: 3,
              }}
            >
              <Typography variant="body2" color="text.secondary">
                {t('shipping')}
              </Typography>
              <Typography variant="body2">
                {t('calculatedAtCheckout')}
              </Typography>
            </Box>

            <Divider sx={{ mb: 3 }} />

            <Box
              sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                mb: 3,
              }}
            >
              <Typography
                variant="h6"
                sx={{
                  fontWeight: 400,
                  letterSpacing: '0.05em',
                }}
              >
                {t('total')}
              </Typography>
              <Typography
                variant="h6"
                sx={{
                  fontWeight: 600,
                  fontSize: '1.3rem',
                }}
              >
                ${totalPrice.toLocaleString()}
              </Typography>
            </Box>

            <Stack spacing={2}>
              <Button
                variant="contained"
                fullWidth
                size="large"
                sx={{
                  py: 1.5,
                  fontSize: '1rem',
                  fontWeight: 500,
                  letterSpacing: '0.05em',
                }}
              >
                {t('checkout')}
              </Button>
              <Button
                variant="outlined"
                fullWidth
                size="large"
                component={Link}
                href={`/${locale}`}
                sx={{
                  py: 1.5,
                  fontSize: '1rem',
                  fontWeight: 500,
                  letterSpacing: '0.05em',
                }}
              >
                {t('continueShopping')}
              </Button>
            </Stack>
          </Paper>
        </Grid>
      </Grid>
    </Container>
  );
} 