import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { buildPlanActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.buildPlan.item,
    itemTypes.buildPlan.actionType,
    itemTypes.buildPlan.uri,
    actionTypes,
    config.proxyRoot
);
