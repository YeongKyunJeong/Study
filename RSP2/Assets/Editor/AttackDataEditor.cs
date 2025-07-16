using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RSP2
{
    [CustomEditor(typeof(AttackData))]
    public class AttackDataEditor : Editor
    {
        private SerializedProperty attackTypeProp;

        #region Melee Attack
        private SerializedProperty hitBoxActivationTimeProp;
        private SerializedProperty hitBoxDeactivationTimeProp;
        private SerializedProperty attackRecoveryTimeProp;

        private SerializedProperty isComboSkillProp;

        private SerializedProperty detectionTypeProp;
        private SerializedProperty colliderSizeProp;
        private SerializedProperty colliderPositionProp;
        #endregion

        #region Range Attack
        private SerializedProperty projectilesProp;

        #endregion

        private void OnEnable()
        {
            attackTypeProp = serializedObject.FindProperty("attackType");


            hitBoxActivationTimeProp = serializedObject.FindProperty("hitBoxActivationTime");
            hitBoxDeactivationTimeProp = serializedObject.FindProperty("hitBoxDeactivationTime");
            attackRecoveryTimeProp = serializedObject.FindProperty("attackRecoveryTime");

            isComboSkillProp = serializedObject.FindProperty("IsComboSkill");

            detectionTypeProp = serializedObject.FindProperty("detectionType");
            colliderSizeProp = serializedObject.FindProperty("colliderSize");
            colliderPositionProp = serializedObject.FindProperty("colliderPosition");

            projectilesProp = serializedObject.FindProperty("projectiles");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(attackTypeProp);

            AttackType attackType = (AttackType)attackTypeProp.enumValueIndex;

            DrawPropertiesExcluding(serializedObject, "attackType", "IsComboSkill", "hitBoxActivationTime", "hitBoxDeactivationTime", "attackRecoveryTime", "detectionType", "colliderSize", "colliderPosition", "projectiles");
            switch (attackType)
            {
                case AttackType.RangeAttackSkill:
                    {
                        EditorGUILayout.PropertyField(projectilesProp);
                        break;
                    }
                default:
                    {
                        EditorGUILayout.PropertyField(hitBoxActivationTimeProp);
                        EditorGUILayout.PropertyField(hitBoxDeactivationTimeProp);
                        EditorGUILayout.PropertyField(attackRecoveryTimeProp);

                        EditorGUILayout.PropertyField(isComboSkillProp);

                        EditorGUILayout.PropertyField(detectionTypeProp);
                        EditorGUILayout.PropertyField(colliderSizeProp);
                        EditorGUILayout.PropertyField(colliderPositionProp);
                        break;
                    }

            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
