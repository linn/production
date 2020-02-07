import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { buildPlanDetailActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.buildPlanDetail.item,
    itemTypes.buildPlanDetail.actionType,
    itemTypes.buildPlanDetail.uri,
    actionTypes,
    config.appRoot
);
