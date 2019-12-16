import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { buildPlanDetailsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.buildPlanDetails.item,
    itemTypes.buildPlanDetails.actionType,
    itemTypes.buildPlanDetails.uri,
    actionTypes,
    config.appRoot
);
