<template>
  <nav-link-item
    title="Home"
    icon="mdi-home-outline"
    :to="{name: 'home'}"
    color="secondary"
    link
    exact
  />

  <nav-link-item 
    title="Stats"
    icon="mdi-chart-donut-variant"
    :to="{name: '/stats/'}"
    link
    exact
    color="secondary"
  />

  <v-tooltip 
    text="Via Web Scrapping"
    location="bottom"
  >
    <template #activator="{props}">
      <nav-link-item
        title="Live Picks"
        icon="mdi-lightning-bolt-outline"
        v-bind="props"
        link
        exact
      />
    </template>
  </v-tooltip>

  <v-tooltip
    text="Via A Neural Network Model"
    location="bottom"
  >
    <template #activator="{props}">
      <nav-link-item  
        title="Generative Picks"
        icon="mdi-brain"
        v-bind="props"
        link
        exact
      />
    </template>
  </v-tooltip>
</template>