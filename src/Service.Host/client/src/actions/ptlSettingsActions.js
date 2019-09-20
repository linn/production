import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { ptlSettingsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.ptlSettings.actionType,
    itemTypes.ptlSettings.uri,
    actionTypes,
    config.appRoot
);
