'use client';

import React from 'react';
import {
  Box,
  Container,
  Typography,
  Button,
  Card,
  CardMedia,
  CardContent,
  Chip,
  Stack,
} from '@mui/material';
import { ArrowForward } from '@mui/icons-material';

const HomePage: React.FC = () => {
  // Mock data for featured products
  const featuredProducts = [
    {
      id: '1',
      name: 'Luxury Silk Blouse',
      brand: 'Ralph Lauren',
      price: 2850,
      image: 'https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=500&q=80',
      category: 'Women',
      isNew: true,
    },
    {
      id: '2',
      name: 'Designer Handbag',
      brand: 'BURBERRY',
      price: 3200,
      image: 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=500&q=80',
      category: 'Accessories',
      isNew: false,
    },
    {
      id: '3',
      name: 'Classic Heels',
      brand: 'JIMMY CHOO',
      price: 4500,
      image: 'https://images.unsplash.com/photo-1544441892-794166f1e3be?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=500&q=80',
      category: 'Women',
      isNew: true,
    },
    {
      id: '4',
      name: 'Evening Bag',
      brand: 'JIMMY CHOO',
      price: 650,
      image: 'https://images.unsplash.com/photo-1511499767150-a48a237f0083?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=500&q=80',
      category: 'Accessories',
      isNew: false,
    },
  ];

  return (
    <Box>
      {/* Hero Section with Categories */}
      <Container maxWidth="xl" sx={{ py: 4 }}>
        <Box
          sx={{
            display: 'flex',
            flexDirection: { xs: 'column', md: 'row' },
            gap: 3,
          }}
        >
          {/* Left Section - Categories */}
          <Box sx={{ flex: 1, pr: { md: 4 } }}>
            <Typography
              variant="h2"
              sx={{
                mb: 6,
                fontWeight: 300,
                letterSpacing: '0.1em',
                fontSize: { xs: '2rem', md: '2.5rem' },
                lineHeight: 1.2,
              }}
            >
              SHOP BY CATEGORY
            </Typography>
            
            <Stack spacing={3}>
              <Typography
                variant="h3"
                sx={{
                  color: '#9E9E9E',
                  fontWeight: 300,
                  fontSize: { xs: '2rem', md: '3rem' },
                  letterSpacing: '0.1em',
                  cursor: 'pointer',
                  transition: 'color 0.3s ease',
                  '&:hover': {
                    color: 'text.primary',
                  },
                }}
              >
                WOMEN
              </Typography>
              <Typography
                variant="h3"
                sx={{
                  color: '#9E9E9E',
                  fontWeight: 300,
                  fontSize: { xs: '2rem', md: '3rem' },
                  letterSpacing: '0.1em',
                  cursor: 'pointer',
                  transition: 'color 0.3s ease',
                  '&:hover': {
                    color: 'text.primary',
                  },
                }}
              >
                MEN
              </Typography>
              <Typography
                variant="h3"
                sx={{
                  color: '#9E9E9E',
                  fontWeight: 300,
                  fontSize: { xs: '2rem', md: '3rem' },
                  letterSpacing: '0.1em',
                  cursor: 'pointer',
                  transition: 'color 0.3s ease',
                  '&:hover': {
                    color: 'text.primary',
                  },
                }}
              >
                KIDS
              </Typography>
              <Typography
                variant="h3"
                sx={{
                  color: '#9E9E9E',
                  fontWeight: 300,
                  fontSize: { xs: '2rem', md: '3rem' },
                  letterSpacing: '0.1em',
                  cursor: 'pointer',
                  transition: 'color 0.3s ease',
                  '&:hover': {
                    color: 'text.primary',
                  },
                }}
              >
                ACCESSORIES
              </Typography>
            </Stack>
          </Box>

          {/* Right Section - Hero Image */}
          <Box sx={{ flex: 1 }}>
            <Box
              sx={{
                height: { xs: 400, md: 600 },
                backgroundImage: 'url(https://images.unsplash.com/photo-1490481651871-ab68de25d43d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1200&q=80)',
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                borderRadius: 0,
              }}
            />
          </Box>
        </Box>
      </Container>

      {/* Brands Section */}
      <Box
        sx={{
          py: 6,
          backgroundColor: '#f8f8f8',
          borderTop: '1px solid #e0e0e0',
          borderBottom: '1px solid #e0e0e0',
        }}
      >
        <Container maxWidth="lg">
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
              gap: { xs: 4, md: 8 },
              flexWrap: 'wrap',
            }}
          >
            {['DIOR', 'PRADA', 'HERMÈS', 'GUCCI', 'Cartier'].map((brand) => (
              <Typography
                key={brand}
                variant="h6"
                sx={{
                  letterSpacing: '0.2em',
                  color: 'text.secondary',
                  fontWeight: 300,
                  fontSize: { xs: '1rem', md: '1.2rem' },
                  fontFamily: 'serif',
                  cursor: 'pointer',
                  transition: 'color 0.3s ease',
                  '&:hover': {
                    color: 'text.primary',
                  },
                }}
              >
                {brand}
              </Typography>
            ))}
          </Box>
        </Container>
      </Box>

      {/* New arrivals Section */}
      <Container maxWidth="lg" sx={{ py: 10 }}>
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            mb: 6,
          }}
        >
          <Typography
            variant="h2"
            sx={{
              fontWeight: 300,
              letterSpacing: '0.1em',
              fontSize: { xs: '1.8rem', md: '2rem' },
            }}
          >
            New arrivals
          </Typography>
          <Button
            variant="text"
            endIcon={<ArrowForward />}
            sx={{
              color: 'text.primary',
              fontSize: '1rem',
              textTransform: 'none',
              '&:hover': {
                backgroundColor: 'transparent',
                textDecoration: 'underline',
              },
            }}
          >
            View all
          </Button>
        </Box>

        <Box
          sx={{
            display: 'flex',
            flexWrap: 'wrap',
            gap: 3,
            justifyContent: 'center',
          }}
        >
          {featuredProducts.map((product) => (
            <Box
              key={product.id}
              sx={{
                flex: '1 1 280px',
                maxWidth: { xs: '100%', sm: 'calc(50% - 12px)', md: 'calc(25% - 12px)' },
                minWidth: '280px',
              }}
            >
              <Card
                sx={{
                  cursor: 'pointer',
                  transition: 'all 0.3s ease',
                  boxShadow: 'none',
                  height: '100%',
                  '&:hover': {
                    transform: 'translateY(-4px)',
                    boxShadow: '0 8px 32px rgba(0, 0, 0, 0.1)',
                  },
                }}
              >
                <Box sx={{ position: 'relative' }}>
                  <CardMedia
                    component="img"
                    height="300"
                    image={product.image}
                    alt={product.name}
                    sx={{
                      objectFit: 'cover',
                    }}
                  />
                  <Box
                    sx={{
                      position: 'absolute',
                      top: 12,
                      right: 12,
                      display: 'flex',
                      gap: 1,
                    }}
                  >
                    <Box
                      sx={{
                        width: 32,
                        height: 32,
                        backgroundColor: 'white',
                        borderRadius: '50%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        cursor: 'pointer',
                        transition: 'transform 0.2s ease',
                        '&:hover': {
                          transform: 'scale(1.1)',
                        },
                      }}
                    >
                      <Typography sx={{ fontSize: '1.2rem' }}>🛒</Typography>
                    </Box>
                    <Box
                      sx={{
                        width: 32,
                        height: 32,
                        backgroundColor: 'white',
                        borderRadius: '50%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        cursor: 'pointer',
                        transition: 'transform 0.2s ease',
                        '&:hover': {
                          transform: 'scale(1.1)',
                        },
                      }}
                    >
                      <Typography sx={{ fontSize: '1.2rem' }}>🤍</Typography>
                    </Box>
                  </Box>
                </Box>
                <CardContent sx={{ p: 2 }}>
                  <Typography
                    variant="caption"
                    sx={{
                      color: 'text.secondary',
                      fontSize: '0.75rem',
                      letterSpacing: '0.1em',
                      mb: 1,
                      display: 'block',
                    }}
                  >
                    {product.brand}
                  </Typography>
                  <Typography
                    variant="h6"
                    sx={{
                      fontWeight: 400,
                      fontSize: '1rem',
                      lineHeight: 1.3,
                      mb: 1,
                    }}
                  >
                    {product.name}
                  </Typography>
                  <Typography
                    variant="h6"
                    sx={{
                      fontWeight: 600,
                      color: 'text.primary',
                      fontSize: '1rem',
                    }}
                  >
                    ${product.price.toLocaleString()}
                  </Typography>
                </CardContent>
              </Card>
            </Box>
          ))}
        </Box>
      </Container>

      {/* Newsletter Section */}
      <Box
        sx={{
          py: 8,
          backgroundColor: '#f8f8f8',
          textAlign: 'center',
        }}
      >
        <Container maxWidth="md">
          <Typography
            variant="h3"
            sx={{
              mb: 3,
              fontWeight: 300,
              letterSpacing: '0.1em',
              fontSize: { xs: '2rem', md: '2.5rem' },
            }}
          >
            Stay Updated
          </Typography>
          <Typography
            variant="body1"
            sx={{
              mb: 4,
              color: 'text.secondary',
              maxWidth: 400,
              mx: 'auto',
              lineHeight: 1.6,
            }}
          >
            Subscribe to our newsletter for the latest fashion trends and exclusive offers.
          </Typography>
          <Button
            variant="contained"
            size="large"
            sx={{
              backgroundColor: 'black',
              color: 'white',
              px: 4,
              py: 1.5,
              fontSize: '1rem',
              fontWeight: 400,
              textTransform: 'none',
              '&:hover': {
                backgroundColor: '#333',
              },
            }}
          >
            Subscribe Now
          </Button>
        </Container>
      </Box>
    </Box>
  );
};

export default HomePage;
