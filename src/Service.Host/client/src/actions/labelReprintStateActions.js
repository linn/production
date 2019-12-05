import { StateApiActions } from '@linn-it/linn-form-components-library';
import { labelReprintActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new StateApiActions(
    itemTypes.labelReprint.item,
    itemTypes.labelReprint.actionType,
    itemTypes.labelReprint.uri,
    actionTypes,
    config.appRoot,
    'application-state'
);
