import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { buildPlansActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.buildPlan.item,
    itemTypes.buildPlan.actionType,
    itemTypes.buildPlan.uri,
    actionTypes,
    config.appRoot
);
