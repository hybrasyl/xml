#!/bin/bash
#
# Workarounds for dumb bugs in xsd2code
#

set -e -x

XSDPATH="XSD/Objects"

git checkout ${XSDPATH}/Castable.cs
git checkout ${XSDPATH}/ElementTableSourceElement.cs
git checkout ${XSDPATH}/ElementTableTargetElement.cs
git checkout ${XSDPATH}/EquipmentRestriction.cs
git checkout ${XSDPATH}/SpawnDamage.cs
git checkout ${XSDPATH}/SpawnDefense.cs
git checkout ${XSDPATH}/MonsterFormulaSet.cs

# Below doesn't seem to be required with xsd2code 6?

#git checkout ${XSDPATH}/StatModifiers.cs
#git checkout ${XSDPATH}/StatModifierFormulas.cs
#git checkout ${XSDPATH}/PlayerFormula.cs
#git checkout ${XSDPATH}/PlayerRegenFormula.cs
#git checkout ${XSDPATH}/StatModifierFormulas.cs


