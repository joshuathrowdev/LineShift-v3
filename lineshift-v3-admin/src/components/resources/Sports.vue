<template>

  <!-- Sports Table -->
  <v-card variant="flat">
    <v-card-text>
      <sports-table />
    </v-card-text>
  </v-card>
  

  <div class="d-flex justify-end w-100 mt-4">
    <v-dialog>
      <template #activator="{props}">
        <v-btn
          v-bind="props"
          color="primary"
          variant="flat"
        >
          Create a New Sport
        </v-btn>
      </template>

      <template #default="{isActive}">
        <div class="d-flex justify-space-around">
          <v-card class="w-50">
            <v-card-text>
              <sport-form>
                <template #closeBtn>
                  <v-btn
                    color="secondary"
                    @click="isActive.value = false"
                  >
                    Close
                  </v-btn>
                </template>  
              </sport-form>
            </v-card-text>
          </v-card>
        </div>
      </template>
    </v-dialog>
  </div>
</template>

<script setup>
import useSportsStore from '@/stores/sports.store';

const {getAllSports} = useSportsStore()


onMounted(async () => {
  await getAllSports()
})
</script>

<route lang="yaml">
  meta:
    requiresAuth: true
</route>