<template>
  <v-list
    density="compact"
  >
    <nav-link-item
      title="Home"
      icon="mdi-home-outline"
    />

    <nav-link-item 
      title="Stats"
      icon="mdi-chart-donut-variant"
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
        />
      </template>
    </v-tooltip>
  </v-list>
</template>