<template>

  <!-- Sports Table -->
  <v-card variant="flat">
    <v-card-text>
      <sports-table />
    </v-card-text>
  </v-card>
  
  <v-card-actions class="d-flex justify-end w-100">
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
  </v-card-actions>
</template>

<script setup>

</script>