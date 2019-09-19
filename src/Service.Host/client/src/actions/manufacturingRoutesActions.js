﻿import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { manufacturingRoutesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.manufacturingRoutes.actionType,
    itemTypes.manufacturingRoutes.uri,
    actionTypes,
    config.appRoot
);
