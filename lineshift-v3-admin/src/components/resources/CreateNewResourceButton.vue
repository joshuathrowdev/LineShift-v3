<template>
  <v-dialog>
    <template #activator="{props}">
      <v-btn
        v-bind="props"
        color="primary"
        variant="flat"
      >
        Create a New {{singleNounResourceKey}}
      </v-btn>
    </template>

    <template #default="{isActive}">
      <div class="d-flex justify-space-around">
        <v-card class="w-50">
          <v-card-text>

            <component :is="resourcePostForm"> 
              <template #closeBtn>
                <v-btn
                  color="secondary"
                  @click="isActive.value = false"
                >
                  Close
                </v-btn>
              </template>
            </component>
            
          </v-card-text>
        </v-card>
      </div>
    </template>
  </v-dialog>
</template>

<script setup>

const props = defineProps(['resourcePostForm', 'singleNounResourceKey' ])

const {resourcePostForm, singleNounResourceKey } = props

</script>
<!-- 
<sport-form>
              <template #closeBtn>
                <v-btn
                  color="secondary"
                  @click="isActive.value = false"
                >
                  Close
                </v-btn>
              </template>  
            </sport-form> -->