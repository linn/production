﻿import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { productionTriggerLevels as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.productionTriggerLevels.item,
    itemTypes.productionTriggerLevels.actionType,
    itemTypes.productionTriggerLevels.uri,
    actionTypes,
    config.appRoot
);
