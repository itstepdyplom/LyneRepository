'use client';

import React, { useEffect, useState } from 'react';
import { Box, Container } from '@mui/material';

interface BrandCarouselProps {
  brands: Array<{
    id: string;
    name: string;
    image: string;
  }>;
}

const BrandCarousel: React.FC<BrandCarouselProps> = ({ brands }) => {
  // Duplicate brands for seamless loop
  const duplicatedBrands = [...brands, ...brands, ...brands]; // Triple for smoother loop

  return (
    <Box
      sx={{
        overflow: 'hidden',
        position: 'relative',
        width: '100%',
        py: 4,
        '&:hover': {
          '& .carousel-track': {
            animationPlayState: 'paused',
          },
        },
      }}
    >
      <Box
        className="carousel-track"
        sx={{
          display: 'flex',
          animation: 'scroll 30s linear infinite',
          width: `${duplicatedBrands.length * 200}px`,
          '@keyframes scroll': {
            '0%': {
              transform: 'translateX(0)',
            },
            '100%': {
              transform: 'translateX(-33.333%)', // Move by 1/3 since we have 3 copies
            },
          },
        }}
      >
        {duplicatedBrands.map((brand, index) => (
          <Box
            key={`${brand.id}-${index}`}
            sx={{
              flex: '0 0 200px',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              px: 3,
              height: '100px',
            }}
          >
            <Box
              component="img"
              src={brand.image}
              alt={brand.name}
              sx={{
                maxWidth: '100%',
                maxHeight: '100%',
                objectFit: 'contain',
                filter: 'grayscale(100%)',
                opacity: 0.6,
                transition: 'all 0.3s ease',
                '&:hover': {
                  filter: 'grayscale(0%)',
                  opacity: 1,
                  transform: 'scale(1.05)',
                },
              }}
            />
          </Box>
        ))}
      </Box>
    </Box>
  );
};

export default BrandCarousel;
